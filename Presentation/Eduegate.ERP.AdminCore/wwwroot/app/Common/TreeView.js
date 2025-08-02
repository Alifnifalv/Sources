/**
 * TreeView - A customizable tree view control with drag and drop functionality
 * Adapted for use with Chart of Accounts AngularJS application
 */

// Check if TreeView is already defined to prevent duplicate declarations
if (typeof window.TreeView === 'undefined') {
    window.TreeView = class TreeView {
        constructor(elementId, data, options = {}) {
            this.container = document.getElementById(elementId);
            if (!this.container) throw new Error(`Element with ID ${elementId} not found`);

            this.data = data || [];
            this.options = {
                dragAndDrop: true,
                allowRenaming: false,
                idField: 'AccountGroupID', // Custom field for our Chart of Accounts
                labelField: 'Name',        // Custom field for our Chart of Accounts
                childrenField: 'children', // Field that contains child nodes
                onNodeSelect: null,
                onNodeMove: null,
                onTreeUpdated: null,
                ...options
            };

            this.draggedNode = null;
            this.draggedElement = null;
            this.dropTarget = null;

            this.init();
        }

        init() {
            console.log("TreeView initialized with", this.data.length, "root nodes");

            // Add base styling
            this.container.classList.add('tree-view');

            // Clear existing content
            this.container.innerHTML = '';

            // Create and append the tree structure
            const treeRoot = document.createElement('ul');
            treeRoot.classList.add('tree-view-root');
            this.container.appendChild(treeRoot);

            // Build the tree from data
            this.buildTree(this.data, treeRoot);

            // Add CSS
            this.addStyles();

            // Initialize event listeners
            this.initEvents();
        }

        buildTree(nodes, parentElement) {
            const { idField, labelField, childrenField } = this.options;

            nodes.forEach(node => {
                const li = document.createElement('li');
                li.classList.add('tree-node');
                li.dataset.id = node[idField];

                if (node.isSelected) {
                    li.classList.add('selected');
                }

                // Create node content wrapper
                const nodeContent = document.createElement('div');
                nodeContent.classList.add('node-content');

                // Add grip handle for drag
                const gripHandle = document.createElement('span');
                gripHandle.innerHTML = '<i class="fa fa-grip-lines"></i>';
                gripHandle.classList.add('drag-handle');
                gripHandle.title = "Drag to rearrange";
                gripHandle.addEventListener('mousedown', (e) => {
                    e.stopPropagation();
                    console.log('Drag handle clicked directly');
                    this.handleDragStart(e);
                });
                nodeContent.appendChild(gripHandle);

                // Toggle icon for expandable nodes
                if (node[childrenField] && node[childrenField].length) {
                    const toggle = document.createElement('i');
                    toggle.classList.add('fa', 'node-icon');

                    if (node.expanded) {
                        toggle.classList.add('fa-minus-circle');
                        toggle.classList.add('text-primary');
                    } else {
                        toggle.classList.add('fa-plus-circle');
                        toggle.classList.add('text-primary');
                    }

                    toggle.onclick = (e) => {
                        e.stopPropagation();
                        this.toggleNode(li, node);
                    };
                    nodeContent.appendChild(toggle);
                } else {
                    // Icon for leaf nodes
                    const icon = document.createElement('i');
                    icon.classList.add('fa', 'fa-circle', 'text-muted', 'node-icon');
                    nodeContent.appendChild(icon);
                }

                // Node label
                const labelSpan = document.createElement('span');
                labelSpan.textContent = node[labelField];
                nodeContent.appendChild(labelSpan);

                li.appendChild(nodeContent);

                // Add children if any and if expanded
                if (node[childrenField] && node[childrenField].length) {
                    const childrenContainer = document.createElement('ul');
                    childrenContainer.classList.add('child-accounts');

                    // Hide if not expanded
                    if (!node.expanded) {
                        childrenContainer.style.display = 'none';
                    }

                    this.buildTree(node[childrenField], childrenContainer);
                    li.appendChild(childrenContainer);
                }

                parentElement.appendChild(li);
            });
        }

        toggleNode(nodeElement, nodeData) {
            const childrenContainer = nodeElement.querySelector('ul');
            const toggle = nodeElement.querySelector('.node-icon');

            if (childrenContainer) {
                const isExpanded = childrenContainer.style.display !== 'none';

                if (isExpanded) {
                    // Collapse
                    childrenContainer.style.display = 'none';
                    toggle.classList.remove('fa-minus-circle');
                    toggle.classList.add('fa-plus-circle');

                    // Update data
                    if (nodeData) {
                        nodeData.expanded = false;
                    }
                } else {
                    // Expand
                    childrenContainer.style.display = 'block';
                    toggle.classList.remove('fa-plus-circle');
                    toggle.classList.add('fa-minus-circle');

                    // Update data
                    if (nodeData) {
                        nodeData.expanded = true;
                    }
                }
            }
        }

        initEvents() {
            if (this.options.dragAndDrop) {
                // Find all drag handles and attach events
                const dragHandles = this.container.querySelectorAll('.drag-handle');

                console.log(`Found ${dragHandles.length} drag handles`);

                if (dragHandles.length === 0) {
                    console.warn("No drag handles found in the tree view!");
                }

                dragHandles.forEach((handle, index) => {
                    console.log(`Setting up drag handle ${index}`);

                    // Remove any existing listeners to prevent duplicates
                    handle.removeEventListener('mousedown', this.handleDragStart.bind(this));

                    // Add the event listener
                    handle.addEventListener('mousedown', (e) => {
                        console.log(`Drag handle ${index} clicked`);
                        this.handleDragStart(e);
                    });

                    // Make it visually obvious
                    handle.style.cursor = 'grab';
                    if (!handle.classList.contains('fa-grip-lines')) {
                        handle.classList.add('fa-grip-lines');
                    }
                });

                // Global move and up handlers
                document.removeEventListener('mousemove', this.handleDragMove.bind(this));
                document.removeEventListener('mouseup', this.handleDragEnd.bind(this));

                document.addEventListener('mousemove', this.handleDragMove.bind(this));
                document.addEventListener('mouseup', this.handleDragEnd.bind(this));

                console.log("Drag and drop event handlers initialized");
            }

            // Node selection event
            this.container.addEventListener('click', (e) => {
                const nodeContent = e.target.closest('.node-content');
                if (nodeContent && !e.target.classList.contains('drag-handle') &&
                    !e.target.classList.contains('node-icon')) {
                    const nodeElement = nodeContent.closest('.tree-node');
                    this.selectNode(nodeElement);
                }
            });

            // Add right-click context menu event listener
            this.container.addEventListener('contextmenu', (e) => {
                // Find the node element that was right-clicked
                const nodeElement = e.target.closest('.tree-node');
                if (nodeElement) {
                    const nodeId = nodeElement.dataset.id;
                    const nodeData = this.findNodeDataById(nodeId);

                    // Dispatch a custom event for the controller to handle
                    if (nodeData && typeof window.showContextMenuForNode === 'function') {
                        window.showContextMenuForNode(e, nodeData);
                    }

                    // Prevent default context menu
                    e.preventDefault();
                    e.stopPropagation();
                    return false;
                }
            });
        }

        handleDragStart(e) {
            // Debug logging
            console.log('Drag start attempt detected');

            e.preventDefault();
            e.stopPropagation();

            // Find the node element
            const nodeElement = e.target.closest('.tree-node');
            if (!nodeElement) {
                console.log('No tree node found to drag');
                return;
            }

            // Store nodes for dragging
            this.draggedElement = nodeElement;
            const nodeId = nodeElement.dataset.id;
            this.draggedNode = this.findNodeDataById(nodeId);

            console.log('Drag start:', this.draggedNode);

            // Add dragging class
            nodeElement.classList.add('dragging');
            document.body.classList.add('tree-dragging');

            // Create drag ghost element
            this.createDragGhost(e, nodeElement);

            // Store initial mouse position
            this.initialMouseX = e.clientX;
            this.initialMouseY = e.clientY;
            this.isDragging = true;

            // Add a custom event to indicate drag started
            const dragStartEvent = new CustomEvent('treeview-dragstart', {
                detail: { nodeId, nodeData: this.draggedNode }
            });
            document.dispatchEvent(dragStartEvent);
        }

        createDragGhost(e, element) {
            // Create ghost if not exists
            let ghost = document.getElementById('drag-ghost');
            if (!ghost) {
                ghost = document.createElement('div');
                ghost.id = 'drag-ghost';
                ghost.className = 'drag-ghost';
                document.body.appendChild(ghost);
            }

            // Set content and position
            const content = element.querySelector('.node-content');
            if (content) {
                const text = content.textContent;
                ghost.textContent = text ? text.trim() : 'Dragging item';
            } else {
                ghost.textContent = 'Dragging item';
            }

            ghost.style.left = e.pageX + 10 + 'px';
            ghost.style.top = e.pageY + 10 + 'px';
            ghost.style.display = 'block';
            ghost.style.zIndex = '9999';

            this.dragGhost = ghost;

            console.log('Drag ghost created at:', e.pageX + 10, e.pageY + 10);
        }

        handleDragMove(e) {
            if (!this.isDragging || !this.draggedElement) {
                return;
            }

            // Move ghost element
            if (this.dragGhost) {
                this.dragGhost.style.left = e.pageX + 10 + 'px';
                this.dragGhost.style.top = e.pageY + 10 + 'px';
                this.dragGhost.style.display = 'block';
            }

            // Find drop target
            const targetElement = this.findDropTarget(e.clientX, e.clientY);

            // Update drop indicators
            this.updateDropIndicator(targetElement, e.clientY);

            // Debug - log position every 20 moves
            if (Math.random() < 0.05) {
                console.log('Dragging at:', e.pageX, e.pageY, 'Drop target:',
                    targetElement ? targetElement.dataset.id : 'none');
            }
        }

        handleDragEnd(e) {
            console.log('Drag end detected');

            if (!this.isDragging) {
                console.log('Not in dragging state');
                return;
            }

            // Hide ghost
            if (this.dragGhost) {
                this.dragGhost.style.display = 'none';
            }

            // Process drop if we have a target
            if (this.draggedElement && this.dropTarget) {
                console.log('Drop target found:', this.dropTarget.dataset.id);
                const dropData = this.getDropData();
                if (dropData && this.isValidDrop(dropData)) {
                    console.log('Valid drop detected, executing move');
                    this.moveNode(dropData);
                } else {
                    console.log('Invalid drop detected:', dropData);
                }
            } else {
                console.log('No valid drop target found');
            }

            // Remove dragging class
            if (this.draggedElement) {
                this.draggedElement.classList.remove('dragging');
            }
            document.body.classList.remove('tree-dragging');

            // Remove drop indicators
            this.removeDropIndicators();

            // Reset state
            this.draggedElement = null;
            this.draggedNode = null;
            this.dropTarget = null;
            this.isDragging = false;

            // Add a custom event to indicate drag ended
            document.dispatchEvent(new CustomEvent('treeview-dragend'));
        }

        findDropTarget(x, y) {
            // Get elements at point
            const elements = document.elementsFromPoint(x, y);

            // Find first tree node
            let targetElement = null;
            for (const el of elements) {
                if (el.classList.contains('tree-node')) {
                    if (el !== this.draggedElement) {
                        targetElement = el;
                        break;
                    }
                }
            }

            this.dropTarget = targetElement;
            return targetElement;
        }

        updateDropIndicator(targetElement, y) {
            // Remove existing indicators
            this.removeDropIndicators();

            if (!targetElement || targetElement === this.draggedElement) return;

            const rect = targetElement.getBoundingClientRect();
            const threshold = rect.height / 3;

            // Determine drop position
            if (y < rect.top + threshold) {
                // Drop before
                targetElement.classList.add('drop-before');
                this.dropPosition = 'before';
            } else if (y > rect.bottom - threshold) {
                // Drop after
                targetElement.classList.add('drop-after');
                this.dropPosition = 'after';
            } else {
                // Drop inside
                targetElement.classList.add('drop-inside');
                this.dropPosition = 'inside';
            }
        }

        removeDropIndicators() {
            const indicators = document.querySelectorAll('.drop-before, .drop-after, .drop-inside');
            indicators.forEach(element => {
                element.classList.remove('drop-before', 'drop-after', 'drop-inside');
            });
        }

        getDropData() {
            if (!this.dropTarget || !this.draggedElement) return null;

            const { idField } = this.options;

            return {
                sourceId: this.draggedElement.dataset.id,
                targetId: this.dropTarget.dataset.id,
                position: this.dropPosition, // 'before', 'after', or 'inside'
                // Additional data for Chart of Accounts
                sourceNode: this.draggedNode,
                targetNode: this.findNodeDataById(this.dropTarget.dataset.id)
            };
        }

        isValidDrop(dropData) {
            const { idField } = this.options;

            // Prevent dropping on itself
            if (dropData.sourceId === dropData.targetId) return false;

            // Prevent dropping parent on its own child (would create circular reference)
            if (this.isAncestor(dropData.sourceId, dropData.targetId)) return false;

            return true;
        }

        isAncestor(parentId, childId) {
            const { idField, childrenField } = this.options;

            const checkChildren = (nodes) => {
                for (const node of nodes) {
                    if (node[idField] == childId) return true;
                    if (node[childrenField] && node[childrenField].length &&
                        checkChildren(node[childrenField])) return true;
                }
                return false;
            };

            const parent = this.findNodeDataById(parentId);
            return parent[childrenField] && checkChildren(parent[childrenField]);
        }

        moveNode(dropData) {
            const { idField, childrenField } = this.options;

            // Extract node IDs
            const sourceId = dropData.sourceId;
            const targetId = dropData.targetId;
            const position = dropData.position;

            console.log('Move node:', sourceId, 'to', position, targetId);

            // 1. Find the relevant nodes and their parents
            const sourceNode = this.findNodeDataById(sourceId);
            const targetNode = this.findNodeDataById(targetId);

            if (!sourceNode || !targetNode) {
                console.error('Could not find source or target node');
                return;
            }

            const sourceParent = this.findParentNode(sourceId);
            const targetParent = this.findParentNode(targetId);

            // 2. Remove the source node from its current parent
            if (sourceParent) {
                sourceParent[childrenField] = sourceParent[childrenField].filter(
                    child => child[idField] != sourceId
                );
            } else {
                // It's a root node
                this.data = this.data.filter(node => node[idField] != sourceId);
            }

            // 3. Add the source node to its new position
            switch (position) {
                case 'inside':
                    // Add as child of target
                    if (!targetNode[childrenField]) targetNode[childrenField] = [];
                    targetNode[childrenField].push(sourceNode);

                    // Update parent reference in source node
                    sourceNode.ParentID = targetNode[idField];
                    break;

                case 'before':
                case 'after':
                    // Add as sibling of target
                    const targetArray = targetParent ? targetParent[childrenField] : this.data;
                    const targetIndex = targetArray.findIndex(node => node[idField] == targetId);

                    // Adjust index for 'after' position
                    const insertIndex = position === 'after' ? targetIndex + 1 : targetIndex;
                    targetArray.splice(insertIndex, 0, sourceNode);

                    // Update parent reference in source node
                    sourceNode.ParentID = targetParent ? targetParent[idField] : null;
                    break;
            }

            // 4. Rebuild the tree
            this.updateTree();

            // 5. Call the callback if provided
            if (this.options.onNodeMove) {
                this.options.onNodeMove(sourceNode, dropData);
            }
        }

        updateTree() {
            // Clear and rebuild the tree view
            const treeRoot = this.container.querySelector('.tree-view-root');
            if (treeRoot) {
                treeRoot.innerHTML = '';
                // Rebuild with the modified data
                this.buildTree(this.data, treeRoot);
            } else {
                console.error('Tree root element not found during update');
            }

            // Notify about update if needed
            if (this.options.onTreeUpdated) {
                this.options.onTreeUpdated(this.data);
            }
        }

        selectNode(nodeElement) {
            const { idField } = this.options;

            // Remove previous selection
            const selectedNodes = this.container.querySelectorAll('.selected');
            selectedNodes.forEach(node => node.classList.remove('selected'));

            // Add selection to current node
            nodeElement.classList.add('selected');

            // Get node data
            const nodeId = nodeElement.dataset.id;
            const nodeData = this.findNodeDataById(nodeId);

            // Update data model to mark as selected
            this.clearAllSelections(this.data);
            if (nodeData) {
                nodeData.isSelected = true;
            }

            // Callback
            if (this.options.onNodeSelect) {
                this.options.onNodeSelect(nodeData, nodeElement);
            }
        }

        clearAllSelections(nodes) {
            if (!nodes) return;

            nodes.forEach(node => {
                node.isSelected = false;
                node.isRightClicked = false;

                if (node.children && node.children.length) {
                    this.clearAllSelections(node.children);
                }
            });
        }

        // Utility methods
        findNodeDataById(id, nodes = this.data) {
            const { idField, childrenField } = this.options;

            for (const node of nodes) {
                if (node[idField] == id) return node;
                if (node[childrenField] && node[childrenField].length) {
                    const found = this.findNodeDataById(id, node[childrenField]);
                    if (found) return found;
                }
            }
            return null;
        }

        findParentNode(childId, nodes = this.data, parent = null) {
            const { idField, childrenField } = this.options;

            for (const node of nodes) {
                if (node[childrenField] && node[childrenField].some(child => child[idField] == childId)) {
                    return node;
                }
                if (node[childrenField] && node[childrenField].length) {
                    const found = this.findParentNode(childId, node[childrenField], node);
                    if (found) return found;
                }
            }
            return parent;
        }

        // Add necessary styles
        addStyles() {
            if (document.getElementById('chart-tree-view-styles')) return;

            const style = document.createElement('style');
            style.id = 'chart-tree-view-styles';
            style.textContent = `
          /* Extra styles for drag and drop */
          .drag-ghost {
            position: fixed;
            opacity: 0.9;
            background: #fff;
            border: 1px solid #0d6efd;
            padding: 5px 10px;
            border-radius: 4px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
            pointer-events: none;
            z-index: 9999;
            max-width: 250px;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
          }
          
          /* Drop indicators */
          .drop-before::before {
            content: '';
            position: absolute;
            left: 0;
            right: 0;
            top: 0;
            height: 2px;
            background-color: #0077ff;
            z-index: 1;
          }
          
          .drop-after::after {
            content: '';
            position: absolute;
            left: 0;
            right: 0;
            bottom: 0;
            height: 2px;
            background-color: #0077ff;
            z-index: 1;
          }
          
          .drop-inside > .node-content {
            background-color: #e0f0ff;
            box-shadow: inset 0 0 0 2px #0077ff;
          }
          
          .tree-node.dragging {
            opacity: 0.5;
            border: 1px dashed #007bff;
            background-color: #e9f7ff;
          }
        `;

            document.head.appendChild(style);
        }
    };

    console.log("TreeView class initialized");
} else {
    console.log("TreeView class already defined, skipping initialization");
}
