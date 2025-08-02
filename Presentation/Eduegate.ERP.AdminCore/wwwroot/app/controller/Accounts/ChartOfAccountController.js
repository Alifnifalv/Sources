app.controller("ChartOfAccountController", ["$scope", "$http", "$compile", "$window", "$timeout", "$location", "$route", "$controller", "$rootScope", function ($scope, $http, $compile, $window, $timeout, $location, $route, $controller, $rootScope) {

    console.log("Enhanced ChartOfAccountController Loaded");

    // Only inherit methods we need from CRUDController, avoid Init conflicts
    try {
        var crudController = $controller('CRUDController', {
            $scope: $scope,
            $compile: $compile,
            $http: $http,
            $timeout: $timeout,
            $window: $window,
            $location: $location,
            $route: $route
        });
    } catch (e) {
        console.warn("CRUDController not available:", e);
    }

    // Initialize variables
    $scope.accounts = [];
    $scope.flatAccountList = [];
    $scope.contextNode = null;
    $scope.selectedLedgerData = null;
    $scope.isLoading = false;
    $scope.isDemoMode = true; // Set to false for production mode
    $scope.ledger = {};
    $scope.AccountTypes = [];
    $scope.selectedAccountType = null;
    $scope.IsCreateLedger = true;
    $scope.searchTerm = '';
    $scope.searchResults = [];
    $scope.originalAccounts = []; // Store original accounts for search
    $scope.parentGroupOptions = []; // For parent group combobox
    $scope.selectedParentGroup = null; // Selected parent group for moving
    $scope.ledgerTransactions = []; // Store ledger transaction data
    $scope.debugExpanded = false; // For debug panel

    // CORRECTED: Load Account Types from the specified API with promise return
    $scope.LoadAccountTypes = function () {
        console.log('Loading account types...');

        return $http({
            method: 'GET',
            url: 'Mutual/GetDynamicLookUpData?lookType=MainGroup&defaultBlank=false'
        }).then(function (result) {
            $scope.AccountTypes = result.data || [];
            console.log('AccountTypes loaded from API:', $scope.AccountTypes);
            return $scope.AccountTypes;
        }).catch(function (error) {
            console.warn('Failed to load account types from API, using fallback:', error);
            // Fallback to default account types
            $scope.AccountTypes = [
                { Key: 1, Value: 'Assets' },
                { Key: 2, Value: 'Liabilities' },
                { Key: 3, Value: 'Equity and Reserves' },
                { Key: 4, Value: 'Revenue' },
                { Key: 5, Value: 'Expenses' }
            ];
            console.log('Using fallback AccountTypes:', $scope.AccountTypes);
            return $scope.AccountTypes;
        });
    };

    console.log('AccountTypes initialized:', $scope.AccountTypes);

    // Custom initialization function to handle the model structure
    $scope.InitializeChartOfAccount = function (model) {
        try {
            console.log("Initializing Chart of Account with model:", model);

            if (model) {
                $scope.viewModel = model;

                // Extract account details if available
                if (model.Details && Array.isArray(model.Details)) {
                    // Process the details array if needed
                    $scope.accountDetails = model.Details;
                }

                // Set chart name if available
                if (model.ChartName) {
                    $scope.chartName = model.ChartName;
                }
            }

            // Load account types first, then chart of accounts
            $scope.LoadAccountTypes();
            $scope.GetChartOfAccounts();

        } catch (error) {
            console.error("Error initializing Chart of Account:", error);
            // Fallback to loading accounts directly
            $scope.LoadAccountTypes();
            $scope.GetChartOfAccounts();
        }
    };

    // Initialize the chart of accounts
    $scope.GetChartOfAccounts = function () {
        $scope.isLoading = true;
        $http.get("Accounts/ChartOfAccount/GetChartOfAccount")
            .then(function (response) {
                console.log("Chart of accounts loaded:", response.data);
                $scope.accounts = $scope.buildTreeData(response.data);
                $scope.originalAccounts = JSON.parse(JSON.stringify($scope.accounts));

                $scope.selectedLedgerData = $scope.accounts[0];
                $scope.initializeTreeView();
                $scope.isLoading = false;
            })
            .catch(function (error) {
                console.error("Error loading chart of accounts:", error);
                $scope.loadDemoData();
                $scope.isLoading = false;
            });
    };

    // Load demo data if API fails
    $scope.loadDemoData = function () {
        console.log("Loading demo chart of accounts data");

        const demoData = [
            { AccountGroupID: 1, AccountCode: '1', AccountGroupName: 'ASSETS', ParentID: null },
            { AccountGroupID: 2, AccountCode: '11', AccountGroupName: 'Current Assets', ParentID: 1 },
            { AccountGroupID: 3, AccountCode: '111', AccountGroupName: 'Cash in Hand', ParentID: 2 },
            { AccountGroupID: 4, AccountCode: '112', AccountGroupName: 'Bank Account', ParentID: 2 },
            { AccountGroupID: 5, AccountCode: '12', AccountGroupName: 'Fixed Assets', ParentID: 1 },
            { AccountGroupID: 6, AccountCode: '2', AccountGroupName: 'LIABILITIES', ParentID: null },
            { AccountGroupID: 7, AccountCode: '21', AccountGroupName: 'Current Liabilities', ParentID: 6 },
            { AccountGroupID: 8, AccountCode: '3', AccountGroupName: 'EQUITY', ParentID: null },
            { AccountGroupID: 9, AccountCode: '4', AccountGroupName: 'REVENUE', ParentID: null },
            { AccountGroupID: 10, AccountCode: '5', AccountGroupName: 'EXPENSES', ParentID: null }
        ];

        $scope.accounts = $scope.buildTreeData(demoData);
        $scope.originalAccounts = JSON.parse(JSON.stringify($scope.accounts));

        if ($scope.accounts.length > 0) {
            $scope.selectedLedgerData = $scope.accounts[0];
        }

        $scope.initializeTreeView();
    };

    // CORRECTED: Enhanced findNodeInTree function with better error handling
    $scope.findNodeInTree = function (accounts, id) {
        if (!accounts || !Array.isArray(accounts)) {
            console.warn('Invalid accounts array provided to findNodeInTree');
            return null;
        }

        if (!id) {
            console.warn('No ID provided to findNodeInTree');
            return null;
        }

        try {
            for (let account of accounts) {
                if (account.id == id || account.AccountGroupID == id) {
                    return account;
                }
                if (account.children) {
                    const found = $scope.findNodeInTree(account.children, id);
                    if (found) return found;
                }
            }
        } catch (error) {
            console.error('Error in findNodeInTree:', error);
        }

        return null;
    };

    // MISSING FUNCTION: Get top parent (root) account type function
    $scope.getTopParent = function (node) {
        console.log('Getting top parent for node:', node);

        if (!node) {
            console.log('No node provided to getTopParent');
            return null;
        }

        function findRootParent(tree, targetNode) {
            for (let rootNode of tree) {
                // Check if this root node IS the target node
                if (rootNode.id === targetNode.id ||
                    rootNode.AccountGroupID === targetNode.AccountGroupID) {
                    console.log('Found direct root match:', rootNode);
                    return rootNode;
                }

                // Check if target node is somewhere in this root's children
                if (rootNode.children) {
                    const found = $scope.findNodeInTree(rootNode.children, targetNode.id || targetNode.AccountGroupID);
                    if (found) {
                        console.log('Found target in children of root:', rootNode);
                        return rootNode; // Return the root, not the found child
                    }
                }
            }
            return null;
        }

        const result = findRootParent($scope.originalAccounts || $scope.accounts, node);
        console.log('Top parent result:', result);
        return result;
    };

    // MISSING FUNCTION: Enhanced getTopParentNameEnhanced function
    $scope.getTopParentNameEnhanced = function () {
        if (!$scope.ledger || !$scope.ledger.under) {
            return 'None';
        }

        try {
            const topParent = $scope.getTopParent($scope.ledger.under);
            const name = topParent ? (topParent.AccountName || topParent.AccountGroupName || 'Unknown') : 'None';
            console.log('Top parent name:', name);
            return name;
        } catch (error) {
            console.error('Error getting top parent name:', error);
            return 'Error';
        }
    };

    // MISSING FUNCTION: Get account type icon
    $scope.getAccountTypeIcon = function (typeKey) {
        const icons = {
            1: 'fa-coins',        // Assets
            2: 'fa-credit-card',  // Liabilities
            3: 'fa-balance-scale', // Equity
            4: 'fa-chart-line',   // Revenue
            5: 'fa-receipt'       // Expenses
        };
        return icons[typeKey] || 'fa-circle';
    };

    // MISSING FUNCTION: Get account type color
    $scope.getAccountTypeColor = function (typeKey) {
        const colors = {
            1: 'primary',    // Assets - Blue
            2: 'warning',    // Liabilities - Orange
            3: 'info',       // Equity - Cyan
            4: 'success',    // Revenue - Green
            5: 'danger'      // Expenses - Red
        };
        return colors[typeKey] || 'secondary';
    };

    // MISSING FUNCTION: Enhanced getSelectedAccountTypeName with error handling
    $scope.getSelectedAccountTypeName = function () {
        try {
            if (!$scope.selectedAccountType || !$scope.AccountTypes) {
                return '';
            }

            const selectedType = $scope.AccountTypes.find(type => type.Key === $scope.selectedAccountType);
            const typeName = selectedType ? selectedType.Value : '';

            console.log('Getting selected account type name:', {
                selectedType: $scope.selectedAccountType,
                typeName: typeName,
                allTypes: $scope.AccountTypes
            });

            return typeName;
        } catch (error) {
            console.error('Error getting account type name:', error);
            return '';
        }
    };

    // Check if node is a leaf (no children)
    $scope.isLeafNode = function (node) {
        if (!node) return false;
        return !node.children || node.children.length === 0;
    };

    // Check button enable conditions based on node type
    $scope.canViewLedger = function (node) {
        return node && $scope.isLeafNode(node);
    };

    $scope.canCreateGroup = function (node) {
        return node !== null; // Always enabled when a node is selected
    };

    $scope.canCreateLedger = function (node) {
        return node && $scope.isLeafNode(node);
    };

    // Get parent group options for selected node
    $scope.updateParentGroupOptions = function (selectedNode) {
        $scope.parentGroupOptions = [];

        if (!selectedNode) return;

        // Find the immediate parent of the selected node
        const parent = $scope.findParentNode($scope.originalAccounts, selectedNode);

        if (parent) {
            // Add siblings (same level nodes) as options
            if (parent.children) {
                parent.children.forEach(sibling => {
                    if (sibling.id !== selectedNode.id) {
                        $scope.parentGroupOptions.push({
                            id: sibling.AccountGroupID,
                            label: sibling.label,
                            name: sibling.AccountName
                        });
                    }
                });
            }

            // Add parent itself as an option
            $scope.parentGroupOptions.unshift({
                id: parent.AccountGroupID,
                label: parent.label,
                name: parent.AccountName
            });
        } else {
            // If no parent, add root level options
            $scope.originalAccounts.forEach(rootNode => {
                if (rootNode.id !== selectedNode.id) {
                    $scope.parentGroupOptions.push({
                        id: rootNode.AccountGroupID,
                        label: rootNode.label,
                        name: rootNode.AccountName
                    });
                }
            });
        }
    };

    // Move account to selected parent group
    $scope.moveToParentGroup = function () {
        if (!$scope.selectedParentGroup || !$scope.selectedLedgerData) {
            alert("Please select a parent group first.");
            return;
        }

        const targetId = $scope.selectedParentGroup;
        const sourceNode = $scope.selectedLedgerData;

        console.log(`Moving ${sourceNode.AccountName} to parent group ${targetId}`);

        // Update database first
        $scope.updateNodeAccountGroup(sourceNode.AccountGroupID, targetId)
            .then(function (result) {
                if (result.success) {
                    // Update client tree structure
                    const success = $scope.updateClientTree(sourceNode, parseInt(sourceNode.id), parseInt(targetId));
                    if (success) {
                        $scope.initializeTreeView();
                        $scope.selectedParentGroup = null; // Reset selection
                        alert(`Successfully moved "${sourceNode.AccountName}" to new parent group.`);
                        $scope.$apply();
                    } else {
                        alert("Failed to update client tree structure");
                    }
                } else {
                    alert("Failed to update database. Please try again.");
                }
            })
            .catch(function (error) {
                console.error("Error moving account:", error);
                alert("Error moving account. Please try again.");
            });
    };

    // Select ledger node and display information
    $scope.selectLedgerNode = function (node) {
        $scope.selectedLedgerData = node;
        console.log("Selected node:", node);

        // Update parent group options for the selected node
        $scope.updateParentGroupOptions(node);

        // Auto-select account type based on top parent
        $scope.autoSelectAccountType(node);

        // Only load ledger data if it's a leaf node
        if ($scope.isLeafNode(node)) {
            $scope.GetLedgerData(node.AccountGroupID);
        } else {
            // Clear table for non-leaf nodes
            $scope.ledgerTransactions = [];
            $timeout(() => {
                window.renderTable();
            }, 100);
        }

        $scope.renderBasicTree();
    };

    // CORRECTED: Enhanced autoSelectAccountType with better parent detection
    $scope.autoSelectAccountType = function (node) {
        console.log('Auto-selecting account type for node:', node);

        try {
            if (!$scope.AccountTypes || $scope.AccountTypes.length === 0) {
                console.log('Account types not available for auto-selection');
                return;
            }

            if (!node) {
                console.log('No node provided for auto-selection');
                return;
            }

            const topParent = $scope.getTopParent(node);
            if (!topParent) {
                console.log('No top parent found for auto-selection');
                return;
            }

            console.log('Top parent for auto-selection:', topParent.AccountName || topParent.AccountGroupName);

            const accountTypeMapping = {
                'ASSETS': 1,
                'ASSET': 1,
                'LIABILITIES': 2,
                'LIABILITY': 2,
                'EQUITY': 3,
                'EQUITY AND RESERVES': 3,
                'REVENUE': 4,
                'INCOME': 4,
                'EXPENSES': 5,
                'EXPENSE': 5
            };

            const parentName = (topParent.AccountName || topParent.AccountGroupName || '').toUpperCase();
            console.log('Checking parent name for auto-selection:', parentName);

            for (let key in accountTypeMapping) {
                if (parentName.includes(key)) {
                    $scope.selectedAccountType = accountTypeMapping[key];
                    console.log('Auto-selected account type:', $scope.selectedAccountType, '(' + key + ')');

                    // Force UI update
                    $scope.$evalAsync();
                    return;
                }
            }

            console.log('No matching account type found for parent:', parentName);
            $scope.selectedAccountType = null;

        } catch (error) {
            console.error('Error in autoSelectAccountType:', error);
            $scope.selectedAccountType = null;
        }
    };

    // Initialize TreeView
    $scope.initializeTreeView = function () {
        $scope.renderBasicTree();
    };

    // Enhanced tree rendering
    $scope.renderBasicTree = function () {
        const container = document.getElementById('tree-container');
        if (container) {
            let html = '<div class="basic-tree">';

            function renderNode(node, level = 0) {
                const indent = level * 20;
                const hasChildren = node.children && node.children.length > 0;
                const expandIcon = hasChildren ? (node.expanded ? '▼' : '▶') : '';
                const isSelected = $scope.selectedLedgerData &&
                    ($scope.selectedLedgerData.AccountGroupID === node.AccountGroupID ||
                        $scope.selectedLedgerData.id === node.id);

                let displayText = node.label;
                if ($scope.searchTerm && $scope.searchTerm.trim() !== '') {
                    const searchRegex = new RegExp(`(${$scope.searchTerm.replace(/[.*+?^${}()|[\]\\]/g, '\\$&')})`, 'gi');
                    displayText = displayText.replace(searchRegex, '<mark>$1</mark>');
                }

                html += `
                    <div class="tree-node-item ${isSelected ? 'selected' : ''}" 
                         style="padding-left: ${indent}px;"
                         onclick="selectNode('${node.id}')"
                         oncontextmenu="showContextMenu(event, '${node.id}'); return false;"
                         draggable="true"
                         ondragstart="handleTreeDragStart(event, '${node.id}')"
                         ondragend="handleTreeDragEnd(event)"
                         ondragover="handleDragOver(event)"
                         ondragenter="handleDragEnter(event, '${node.id}')"
                         ondragleave="handleDragLeave(event)"
                         ondrop="handleDropEvent(event, '${node.id}')">
                        
                        <span class="expand-icon"
                              onclick="event.stopPropagation(); toggleExpansion('${node.id}')">
                            ${expandIcon}
                        </span>
                        
                        <i class="fas ${hasChildren ? 'fa-folder' : 'fa-file'} node-icon"></i>
                        
                        <span class="node-text">${displayText}</span>
                    </div>
                `;

                if (hasChildren && node.expanded) {
                    node.children.forEach(child => {
                        renderNode(child, level + 1);
                    });
                }
            }

            $scope.accounts.forEach(account => {
                renderNode(account);
            });

            html += '</div>';
            container.innerHTML = html;
            $scope.updateFlatAccountList();
        }
    };

    // Create flat account list for dropdowns
    $scope.updateFlatAccountList = function () {
        $scope.flatAccountList = [];

        function flattenTree(tree, level = 0) {
            tree.forEach(node => {
                const indent = '  '.repeat(level);
                $scope.flatAccountList.push({
                    ...node,
                    displayName: `${indent}${node.label}`,
                    level: level
                });

                if (node.children && node.children.length > 0) {
                    flattenTree(node.children, level + 1);
                }
            });
        }

        flattenTree($scope.accounts);
    };

    // Expand all nodes
    $scope.expandAll = function () {
        function expandAllNodes(tree) {
            tree.forEach(node => {
                node.expanded = true;
                if (node.children && node.children.length > 0) {
                    expandAllNodes(node.children);
                }
            });
        }

        expandAllNodes($scope.accounts);
        $scope.initializeTreeView();
    };

    // Collapse all nodes
    $scope.collapseAll = function () {
        function collapseAllNodes(tree) {
            tree.forEach(node => {
                node.expanded = false;
                if (node.children && node.children.length > 0) {
                    collapseAllNodes(node.children);
                }
            });
        }

        collapseAllNodes($scope.accounts);
        $scope.initializeTreeView();
    };

    // Search accounts
    $scope.searchAccounts = function () {
        if (!$scope.searchTerm || $scope.searchTerm.trim() === '') {
            $scope.clearSearch();
            return;
        }

        const searchTerm = $scope.searchTerm.toLowerCase();
        $scope.searchResults = [];

        function searchInTree(tree, searchTerm) {
            const results = [];

            tree.forEach(node => {
                const matches = node.label.toLowerCase().includes(searchTerm) ||
                    node.AccountName.toLowerCase().includes(searchTerm) ||
                    node.AccountCode.toLowerCase().includes(searchTerm);

                if (matches) {
                    results.push(node);
                }

                if (node.children && node.children.length > 0) {
                    const childResults = searchInTree(node.children, searchTerm);
                    results.push(...childResults);
                }
            });

            return results;
        }

        $scope.searchResults = searchInTree($scope.originalAccounts, searchTerm);
        $scope.createFilteredTree();
        $scope.initializeTreeView();
    };

    // Create filtered tree for search results
    $scope.createFilteredTree = function () {
        if ($scope.searchResults.length === 0) return;

        const filteredTree = [];
        const addedNodeIds = new Set();

        $scope.searchResults.forEach(result => {
            $scope.addNodeWithParents(result, filteredTree, addedNodeIds);
        });

        $scope.accounts = filteredTree;
    };

    // Add node with all its parents to filtered tree
    $scope.addNodeWithParents = function (node, tree, addedNodeIds) {
        if (addedNodeIds.has(node.id)) return;

        const parent = $scope.findParentNode($scope.originalAccounts, node);

        if (parent) {
            $scope.addNodeWithParents(parent, tree, addedNodeIds);

            const parentInFiltered = $scope.findNodeInTree(tree, parent.id);
            if (parentInFiltered) {
                if (!parentInFiltered.children) {
                    parentInFiltered.children = [];
                }
                if (!parentInFiltered.children.find(child => child.id === node.id)) {
                    parentInFiltered.children.push(JSON.parse(JSON.stringify(node)));
                    parentInFiltered.expanded = true;
                }
            }
        } else {
            if (!tree.find(n => n.id === node.id)) {
                const nodeCopy = JSON.parse(JSON.stringify(node));
                nodeCopy.expanded = true;
                tree.push(nodeCopy);
            }
        }

        addedNodeIds.add(node.id);
    };

    // Find parent node
    $scope.findParentNode = function (tree, targetNode) {
        for (let node of tree) {
            if (node.children) {
                if (node.children.find(child => child.id === targetNode.id)) {
                    return node;
                }
                const parent = $scope.findParentNode(node.children, targetNode);
                if (parent) return parent;
            }
        }
        return null;
    };

    // Clear search
    $scope.clearSearch = function () {
        $scope.searchTerm = '';
        $scope.searchResults = [];
        $scope.accounts = JSON.parse(JSON.stringify($scope.originalAccounts));
        $scope.initializeTreeView();
    };

    // Context menu functionality
    $scope.showContextMenu = function ($event, account) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.contextNode = account;
        $scope.hideContextMenu();

        const menu = document.getElementById("contextMenu");
        if (menu) {
            const menuWidth = 200;
            const menuHeight = 150;
            const windowWidth = window.innerWidth;
            const windowHeight = window.innerHeight;

            let left = $event.pageX;
            let top = $event.pageY;

            if (left + menuWidth > windowWidth) {
                left = windowWidth - menuWidth - 10;
            }
            if (top + menuHeight > windowHeight) {
                top = windowHeight - menuHeight - 10;
            }

            menu.style.display = "block";
            menu.style.left = `${left}px`;
            menu.style.top = `${top}px`;
            menu.style.zIndex = "9999";
        }

        $timeout(() => {
            document.addEventListener('click', $scope.globalClickHandler);
            document.addEventListener('contextmenu', $scope.globalClickHandler);
        }, 0);
    };

    $scope.globalClickHandler = function (event) {
        const menu = document.getElementById("contextMenu");
        if (menu && !menu.contains(event.target)) {
            $scope.hideContextMenu();
        }
    };

    $scope.hideContextMenu = function () {
        const menu = document.getElementById("contextMenu");
        if (menu) {
            menu.style.display = 'none';
        }
        document.removeEventListener('click', $scope.globalClickHandler);
        document.removeEventListener('contextmenu', $scope.globalClickHandler);
    };

    // View ledger data action
    $scope.viewLedgerData = function (node) {
        $scope.selectLedgerNode(node);
        $scope.hideContextMenu();

        if ($scope.isLeafNode(node)) {
            $scope.GetLedgerData(node.AccountGroupID);
        }
    };

    // CORRECTED: Get Ledger Data using $http instead of jQuery
    $scope.GetLedgerData = function (groupID) {
        $scope.isLoadingLedger = true;

        $http({
            method: "GET",
            url: "Accounts/ChartOfAccount/GetLedgerTransactions?groupID=" + groupID
        }).then(function (response) {
            console.log("Ledger transactions loaded:", response.data);
            $scope.ledgerTransactions = response.data || [];

            // Update global tableData for rendering
            window.tableData = $scope.ledgerTransactions;

            $scope.isLoadingLedger = false;

            $timeout(() => {
                window.renderTable();
            }, 100);

        }).catch(function (error) {
            console.error("Error fetching ledger transactions:", error);
            $scope.isLoadingLedger = false;

            // Load demo data on error
            $scope.ledgerTransactions = $scope.generateDemoLedgerData($scope.selectedLedgerData);
            window.tableData = $scope.ledgerTransactions;

            $timeout(() => {
                window.renderTable();
            }, 100);
        });
    };

    // Generate demo ledger data
    $scope.generateDemoLedgerData = function (account) {
        const transactions = [
            {
                GroupID: 1,
                LedgerCode: '1001',
                LedgerName: 'Cash in Hand',
                LedgerGroup: 'Current Assets',
                OpeningBalance: 50000,
                Debit: 25000,
                Credit: 10000,
                ClosingBalance: 65000
            },
            {
                GroupID: 2,
                LedgerCode: '1002',
                LedgerName: 'Bank Account',
                LedgerGroup: 'Current Assets',
                OpeningBalance: 100000,
                Debit: 50000,
                Credit: 30000,
                ClosingBalance: 120000
            },
            {
                GroupID: 3,
                LedgerCode: '2001',
                LedgerName: 'Accounts Payable',
                LedgerGroup: 'Current Liabilities',
                OpeningBalance: 0,
                Debit: 15000,
                Credit: 45000,
                ClosingBalance: -30000
            },
            {
                GroupID: 4,
                LedgerCode: '4001',
                LedgerName: 'Sales Revenue',
                LedgerGroup: 'Revenue',
                OpeningBalance: 0,
                Debit: 0,
                Credit: 200000,
                ClosingBalance: -200000
            },
            {
                GroupID: 5,
                LedgerCode: '5001',
                LedgerName: 'Office Expenses',
                LedgerGroup: 'Expenses',
                OpeningBalance: 0,
                Debit: 75000,
                Credit: 0,
                ClosingBalance: 75000
            }
        ];

        return transactions;
    };

    // CORRECTED: Enhanced modal opening functions with proper data loading
    $scope.OpenCreateAccount = function (node) {
        console.log('Opening Create Account for node:', node);
        $scope.hideContextMenu();
        $scope.IsCreateLedger = true;

        // Reset form first
        $scope.resetFormData();

        // Load account types first, then get account code
        if (!$scope.AccountTypes || $scope.AccountTypes.length === 0) {
            $scope.LoadAccountTypes().then(function () {
                $scope.GetAccountCode(node);
            });
        } else {
            $scope.GetAccountCode(node);
        }
    };

    $scope.OpenCreateGroup = function (node) {
        console.log('Opening Create Group for node:', node);
        $scope.hideContextMenu();
        $scope.IsCreateLedger = false;

        // Reset form first
        $scope.resetFormData();

        // Load account types first, then get account code
        if (!$scope.AccountTypes || $scope.AccountTypes.length === 0) {
            $scope.LoadAccountTypes().then(function () {
                $scope.GetAccountCode(node);
            });
        } else {
            $scope.GetAccountCode(node);
        }
    };

    // CORRECTED: Get Account Code with proper API URLs
    $scope.GetAccountCode = function (selectedNode) {
        console.log('Getting account code for node:', selectedNode);

        var accountCode = "0";
        var url = "";

        if ($scope.IsCreateLedger === true) {
            url = "Schools/School/GetAccountCodeByGroup?GroupID=" + selectedNode.AccountGroupID;
        } else {
            url = "Schools/School/GetGroupCodeByParentGroup?parentGroupID=" + selectedNode.AccountGroupID;
        }

        $http({
            method: "GET",
            url: url
        }).then(function (response) {
            console.log('Account code API response:', response.data);

            if ($scope.IsCreateLedger === true) {
                accountCode = response.data.AccountCode || "AUTO";
            } else {
                accountCode = response.data.GroupCode || "AUTO";
            }

            console.log('Generated account code:', accountCode);
            $scope.FillAccount(selectedNode, accountCode);

        }).catch(function (error) {
            console.error("Error fetching account code:", error);
            // Use fallback logic
            accountCode = $scope.generateFallbackCode(selectedNode);
            console.log('Using fallback code:', accountCode);
            $scope.FillAccount(selectedNode, accountCode);
        });
    };

    // Generate fallback code if API fails
    $scope.generateFallbackCode = function (selectedNode) {
        console.log('Generating fallback code for:', selectedNode);

        if ($scope.IsCreateLedger) {
            // For accounts, add 01 to parent code
            const baseCode = selectedNode.AccountCode || "1";
            return baseCode + "01";
        } else {
            // For groups, increment parent code by 1
            const parentCode = parseInt(selectedNode.AccountCode) || 1;
            return (parentCode + 1).toString();
        }
    };

    // CORRECTED: Fixed FillAccount function to properly populate all fields
    $scope.FillAccount = function (selectedNode, accountCode) {
        console.log('Filling account form with:', {
            selectedNode: selectedNode,
            accountCode: accountCode,
            IsCreateLedger: $scope.IsCreateLedger
        });

        // Ensure AccountTypes are loaded before opening modal
        if (!$scope.AccountTypes || $scope.AccountTypes.length === 0) {
            console.log('Account types not loaded, loading them first...');
            $scope.LoadAccountTypes().then(function () {
                $scope.proceedWithFillAccount(selectedNode, accountCode);
            });
        } else {
            $scope.proceedWithFillAccount(selectedNode, accountCode);
        }
    };

    // CORRECTED: Separate function to proceed with filling account after types are loaded
    $scope.proceedWithFillAccount = function (selectedNode, accountCode) {
        const underDisplayText = selectedNode.label ||
            `${selectedNode.AccountCode} - ${selectedNode.AccountName || selectedNode.AccountGroupName}`;

        // Clear and rebuild ledger object
        $scope.ledger = {
            headNo: accountCode,
            name: '',
            under: selectedNode,
            underDisplay: underDisplayText
        };

        console.log('Ledger object created:', $scope.ledger);

        // Auto-select account type based on parent
        $scope.autoSelectAccountType(selectedNode);

        // Force Angular to digest the changes
        $scope.$evalAsync(function () {
            console.log('Opening modal with data:', {
                headNo: $scope.ledger.headNo,
                underDisplay: $scope.ledger.underDisplay,
                selectedAccountType: $scope.selectedAccountType,
                accountTypesCount: $scope.AccountTypes.length
            });

            // Open modal with slight delay to ensure data is set
            $timeout(function () {
                $("#ledgerModal").modal("show");

                // Force update UI after modal is shown
                $timeout(function () {
                    $scope.$apply();
                }, 100);
            }, 150);
        });
    };

    // MISSING FUNCTION: Enhanced resetFormData function
    $scope.resetFormData = function () {
        console.log('Resetting form data');
        $scope.selectedAccountType = null;
        $scope.ledger = {
            headNo: '',
            name: '',
            under: null,
            underDisplay: ''
        };
        $scope.debugExpanded = false;
    };

    // MISSING FUNCTION: Add refresh function for account types
    $scope.refreshAccountTypes = function () {
        console.log('Refreshing account types display');
        $scope.LoadAccountTypes().then(function () {
            if ($scope.ledger && $scope.ledger.under) {
                $scope.autoSelectAccountType($scope.ledger.under);
            }
            $scope.$apply();
        });
    };

    // MISSING FUNCTION: Enhanced debug functions
    $scope.debugSelectionEnhanced = function () {
        console.log('=== ENHANCED DEBUG SELECTION ===');
        try {
            console.log('Ledger Object:', $scope.ledger);
            console.log('Selected Node (under):', $scope.ledger?.under);
            console.log('Under Display:', $scope.ledger?.underDisplay);
            console.log('Head No (Code):', $scope.ledger?.headNo);

            if ($scope.ledger?.under) {
                console.log('Top Parent:', $scope.getTopParent($scope.ledger.under));
            }

            console.log('Selected Account Type:', $scope.selectedAccountType);
            console.log('Account Types Available:', $scope.AccountTypes);
            console.log('Account Types Count:', $scope.AccountTypes?.length);
            console.log('Is Create Ledger:', $scope.IsCreateLedger);
            console.log('Modal Visible:', $('#ledgerModal').hasClass('show'));
        } catch (error) {
            console.error('Error in debug function:', error);
        }
    };

    // MISSING FUNCTION: Enhanced forceAutoSelectEnhanced function
    $scope.forceAutoSelectEnhanced = function () {
        console.log('Force auto-selecting account type');
        try {
            if ($scope.ledger && $scope.ledger.under) {
                $scope.autoSelectAccountType($scope.ledger.under);
                $scope.$apply();
            } else {
                console.log('No under account selected for auto-selection');
            }
        } catch (error) {
            console.error('Error in force auto-select:', error);
        }
    };

    // MISSING FUNCTION: Enhanced onAccountTypeChange function
    $scope.onAccountTypeChange = function (typeKey) {
        console.log('Account type manually changed to:', typeKey);
        try {
            $scope.selectedAccountType = parseInt(typeKey);

            // Force UI update
            $scope.$evalAsync(function () {
                console.log('Account type selection confirmed:', $scope.selectedAccountType);
            });
        } catch (error) {
            console.error('Error changing account type:', error);
        }
    };

    // CORRECTED: Save account/group with proper API integration
    $scope.SaveAccounts = function () {
        if (!$scope.ledger.name || !$scope.ledger.headNo) {
            alert("Please fill all required fields.");
            return;
        }

        if (!$scope.selectedAccountType) {
            alert("Please select an account type.");
            return;
        }

        console.log('Saving account with type:', $scope.selectedAccountType, $scope.getSelectedAccountTypeName());

        const saveData = {
            AccountCode: $scope.ledger.headNo,
            AccountName: $scope.ledger.name,
            ParentID: $scope.ledger.under.AccountGroupID,
            AccountType: $scope.selectedAccountType,
            IsGroup: !$scope.IsCreateLedger
        };

        // Call API to save account
        const apiUrl = $scope.IsCreateLedger ?
            "Accounts/ChartOfAccount/CreateAccount" :
            "Accounts/ChartOfAccount/CreateGroup";

        $http.post(apiUrl, saveData)
            .then(function (response) {
                console.log('Account/Group saved successfully:', response.data);

                // Create new node for client-side update
                const newNode = {
                    id: response.data.AccountGroupID?.toString() || Date.now().toString(),
                    label: `${$scope.ledger.headNo} - ${$scope.ledger.name}`,
                    AccountGroupID: response.data.AccountGroupID || Date.now(),
                    AccountName: $scope.ledger.name,
                    AccountGroupName: $scope.ledger.name,
                    AccountCode: $scope.ledger.headNo,
                    ParentID: $scope.ledger.under.AccountGroupID,
                    AccountType: $scope.selectedAccountType,
                    expanded: false
                };

                // Add to tree structure
                $scope.addNewNodeToTree(newNode);

                alert(`${$scope.IsCreateLedger ? 'Account' : 'Group'} "${$scope.ledger.name}" created successfully as ${$scope.getSelectedAccountTypeName()}!`);
                $scope.closeModal();

            })
            .catch(function (error) {
                console.error('Error saving account/group:', error);

                // Fallback to client-side only update in demo mode
                if ($scope.isDemoMode) {
                    const newNode = {
                        id: Date.now().toString(),
                        label: `${$scope.ledger.headNo} - ${$scope.ledger.name}`,
                        AccountGroupID: Date.now(),
                        AccountName: $scope.ledger.name,
                        AccountGroupName: $scope.ledger.name,
                        AccountCode: $scope.ledger.headNo,
                        ParentID: $scope.ledger.under.AccountGroupID,
                        AccountType: $scope.selectedAccountType,
                        expanded: false
                    };

                    $scope.addNewNodeToTree(newNode);
                    alert(`${$scope.IsCreateLedger ? 'Account' : 'Group'} "${$scope.ledger.name}" created successfully (Demo Mode)!`);
                    $scope.closeModal();
                } else {
                    alert("Error saving account. Please try again.");
                }
            });
    };

    // Add new node to tree structure
    $scope.addNewNodeToTree = function (newNode) {
        function addToParent(tree, parentId, newNode) {
            for (let node of tree) {
                if (node.AccountGroupID === parentId) {
                    if (!node.children) {
                        node.children = [];
                    }
                    node.children.push(newNode);
                    node.expanded = true;
                    return true;
                }
                if (node.children && addToParent(node.children, parentId, newNode)) {
                    return true;
                }
            }
            return false;
        }

        addToParent($scope.accounts, $scope.ledger.under.AccountGroupID, newNode);
        addToParent($scope.originalAccounts, $scope.ledger.under.AccountGroupID, JSON.parse(JSON.stringify(newNode)));

        $scope.initializeTreeView();
    };

    // CORRECTED: Reset ledger form and close modal properly
    $scope.resetLedger = function () {
        $scope.closeModal();
    };

    // CORRECTED: Close modal properly
    $scope.closeModal = function () {
        $scope.ledger = {};
        $scope.selectedAccountType = null;
        $scope.debugExpanded = false;
        $("#ledgerModal").modal("hide");
    };

    // Refresh the tree
    $scope.refreshTree = function () {
        $scope.searchTerm = '';
        $scope.searchResults = [];
        $scope.GetChartOfAccounts();
    };

    // Database update function for table item drop
    $scope.updateTableItemAccountGroup = function (itemId, groupId) {
        if ($scope.isDemoMode) {
            console.log(`Demo Mode: Would update table item ${itemId} to AccountGroupID ${groupId}`);
            return Promise.resolve({ success: true });
        }

        // Production mode - actual API call
        return $http.post('/api/UpdateTableItemAccountGroup', {
            ItemId: itemId,
            AccountGroupId: groupId
        }).then(function (response) {
            console.log('Table item account group updated successfully:', response.data);
            return response.data;
        }).catch(function (error) {
            console.error('Error updating table item account group:', error);
            throw error;
        });
    };

    // Database update function for node move
    $scope.updateNodeAccountGroup = function (accountId, newAccountGroupId) {
        if ($scope.isDemoMode) {
            console.log(`Demo Mode: Would update AccountID ${accountId} to AccountGroupID ${newAccountGroupId}`);
            return Promise.resolve({ success: true });
        }

        // Production mode - actual API call
        return $http.post('/api/UpdateNodeAccountGroup', {
            AccountId: accountId,
            NewAccountGroupId: newAccountGroupId
        }).then(function (response) {
            console.log('Node account group updated successfully:', response.data);
            return response.data;
        }).catch(function (error) {
            console.error('Error updating node account group:', error);
            throw error;
        });
    };

    // Handle tree node move
    $scope.handleTreeNodeMove = function (sourceNode, targetId) {
        const sourceId = parseInt(sourceNode.id);
        const targetIdInt = parseInt(targetId);

        console.log(`Moving tree node ${sourceId} under ${targetIdInt}`);

        // Basic validation
        if (sourceId === targetIdInt) {
            alert("Cannot move account under itself");
            return;
        }

        // Check for circular reference
        if ($scope.isCircularReference(sourceId, targetIdInt)) {
            alert("Cannot move account under its own descendant");
            return;
        }

        // Find target node
        const targetNode = $scope.findNodeInTree($scope.accounts, targetId);
        if (!targetNode) {
            alert("Target account not found");
            return;
        }

        // Check if target can accept children (is a group)
        if ($scope.isLeafNode(targetNode) && !$scope.isMainGroup(targetNode)) {
            alert("Cannot move account under a leaf account. Try moving to a group account instead.");
            return;
        }

        // Update database first
        $scope.updateNodeAccountGroup(sourceNode.AccountGroupID, targetNode.AccountGroupID)
            .then(function (result) {
                if (result.success) {
                    // Perform the client-side move
                    const success = $scope.updateClientTree(sourceNode, sourceId, targetIdInt);
                    if (success) {
                        console.log(`Successfully moved ${sourceNode.AccountName} under ${targetNode.AccountName}`);
                        $scope.initializeTreeView();
                        $scope.$apply();
                        alert(`Successfully moved "${sourceNode.AccountName || sourceNode.AccountGroupName}" under "${targetNode.AccountName || targetNode.AccountGroupName}"`);
                    } else {
                        alert("Failed to update client tree structure");
                    }
                } else {
                    alert("Failed to update database. Please try again.");
                }
            })
            .catch(function (error) {
                console.error("Error moving node:", error);
                alert("Error moving account. Please try again.");
            });
    };

    // Check for circular reference
    $scope.isCircularReference = function (sourceId, targetId) {
        function findNodeById(nodes, id) {
            for (let node of nodes) {
                if (parseInt(node.id) === id) {
                    return node;
                }
                if (node.children) {
                    const found = findNodeById(node.children, id);
                    if (found) return found;
                }
            }
            return null;
        }

        function isDescendant(parentNode, targetId) {
            if (!parentNode.children) return false;

            for (let child of parentNode.children) {
                if (parseInt(child.id) === targetId) return true;
                if (isDescendant(child, targetId)) return true;
            }
            return false;
        }

        const sourceNode = findNodeById($scope.accounts, sourceId);
        return sourceNode ? isDescendant(sourceNode, targetId) : false;
    };

    // Check if node is a main group that can accept children
    $scope.isMainGroup = function (node) {
        if (!node) return false;
        const mainGroups = ['ASSETS', 'LIABILITIES', 'EQUITY', 'REVENUE', 'EXPENSES'];
        const nodeName = node.AccountName || node.AccountGroupName || '';
        return mainGroups.some(group =>
            nodeName.toUpperCase().includes(group)
        );
    };

    // Update client tree structure
    $scope.updateClientTree = function (node, sourceId, targetId) {
        try {
            function removeNode(tree, nodeId) {
                for (let i = 0; i < tree.length; i++) {
                    if (parseInt(tree[i].id) === nodeId) {
                        const removedNode = tree.splice(i, 1)[0];
                        return removedNode;
                    }
                    if (tree[i].children && tree[i].children.length > 0) {
                        const removedNode = removeNode(tree[i].children, nodeId);
                        if (removedNode) return removedNode;
                    }
                }
                return null;
            }

            function addNodeAsChild(tree, targetId, newNode) {
                for (let i = 0; i < tree.length; i++) {
                    if (parseInt(tree[i].id) === targetId) {
                        if (!tree[i].children) {
                            tree[i].children = [];
                        }
                        tree[i].children.push(newNode);
                        tree[i].expanded = true;
                        return true;
                    }
                    if (tree[i].children && addNodeAsChild(tree[i].children, targetId, newNode)) {
                        return true;
                    }
                }
                return false;
            }

            // Remove node from current position
            const removedNode = removeNode($scope.accounts, sourceId);

            if (!removedNode) {
                console.error("Could not find source node to remove");
                return false;
            }

            // Add node to new position
            const added = addNodeAsChild($scope.accounts, targetId, removedNode);

            if (!added) {
                console.error("Could not add node to target position");
                // Try to restore to original position if possible
                $scope.accounts.push(removedNode);
                return false;
            }

            // Update original accounts as well
            const originalRemovedNode = removeNode($scope.originalAccounts, sourceId);
            if (originalRemovedNode) {
                addNodeAsChild($scope.originalAccounts, targetId, originalRemovedNode);
            }

            console.log("Tree structure updated successfully");
            return true;

        } catch (error) {
            console.error("Error updating client tree:", error);
            return false;
        }
    };

    // Handle table item drop
    $scope.handleTableItemDrop = function (targetId, draggedItem) {
        if (!draggedItem) return;

        const targetAccount = $scope.findNodeInTree($scope.accounts, targetId);
        if (!targetAccount) {
            alert("Target account not found");
            return;
        }

        // Update database first
        $scope.updateTableItemAccountGroup(draggedItem.GroupID, targetAccount.AccountGroupID)
            .then(function (result) {
                if (result.success) {
                    const newAccount = {
                        id: Date.now().toString(),
                        label: `${draggedItem.LedgerCode} - ${draggedItem.LedgerName}`,
                        AccountGroupID: Date.now(),
                        AccountName: draggedItem.LedgerName,
                        AccountGroupName: draggedItem.LedgerName,
                        AccountCode: draggedItem.LedgerCode,
                        ParentID: targetAccount.AccountGroupID,
                        balance: draggedItem.ClosingBalance || 0
                    };

                    if (!targetAccount.children) {
                        targetAccount.children = [];
                    }
                    targetAccount.children.push(newAccount);
                    targetAccount.expanded = true;

                    // Remove from table data
                    const tableIndex = window.tableData.findIndex(item => item.GroupID === draggedItem.GroupID);
                    if (tableIndex > -1) {
                        window.tableData.splice(tableIndex, 1);
                        window.renderTable();
                    }

                    $scope.initializeTreeView();
                    alert(`Successfully moved "${draggedItem.LedgerName}" to "${targetAccount.AccountName || targetAccount.AccountGroupName}"`);
                    $scope.$apply();
                } else {
                    alert("Failed to update database. Please try again.");
                }
            })
            .catch(function (error) {
                console.error("Error updating table item:", error);
                alert("Error moving item. Please try again.");
            });
    };

    // Delete account with confirmation
    $scope.deleteAccount = function (node) {
        $scope.hideContextMenu();

        if (!node || !node.AccountGroupID) {
            alert("Invalid account selected");
            return;
        }

        // Check if node has children
        if (node.children && node.children.length > 0) {
            alert("Cannot delete account with sub-accounts. Please move or delete child accounts first.");
            return;
        }

        const confirmMessage = `Are you sure you want to delete "${node.AccountName || node.AccountGroupName}"?\n\nThis action cannot be undone.`;

        if (confirm(confirmMessage)) {
            const success = $scope.removeNodeFromTree(parseInt(node.id));
            if (success) {
                $scope.initializeTreeView();

                // Clear selection if deleted node was selected
                if ($scope.selectedLedgerData && $scope.selectedLedgerData.AccountGroupID === node.AccountGroupID) {
                    $scope.selectedLedgerData = null;
                }

                alert(`Account "${node.AccountName || node.AccountGroupName}" deleted successfully.`);
                $scope.$apply();
            } else {
                alert("Failed to delete account");
            }
        }
    };

    // Remove node from tree structure
    $scope.removeNodeFromTree = function (nodeId) {
        function removeFromArray(tree) {
            for (let i = 0; i < tree.length; i++) {
                if (parseInt(tree[i].id) === nodeId) {
                    tree.splice(i, 1);
                    return true;
                }
                if (tree[i].children && removeFromArray(tree[i].children)) {
                    return true;
                }
            }
            return false;
        }

        // Remove from both current and original accounts
        const removed1 = removeFromArray($scope.accounts);
        const removed2 = removeFromArray($scope.originalAccounts);

        return removed1 || removed2;
    };

    // Build tree data structure from flat array
    $scope.buildTreeData = function (accounts) {
        if (!accounts || !Array.isArray(accounts)) {
            console.warn("Invalid accounts data:", accounts);
            return [];
        }

        let map = {}, tree = [];

        // Create map of all accounts
        accounts.forEach(acc => {
            map[acc.AccountGroupID] = {
                id: acc.AccountGroupID.toString(),
                label: `${acc.AccountCode} - ${acc.AccountGroupName}`,
                AccountGroupID: acc.AccountGroupID,
                AccountName: acc.AccountGroupName,
                AccountGroupName: acc.AccountGroupName,
                AccountCode: acc.AccountCode,
                ParentID: acc.ParentID,
                expanded: false,
                children: []
            };
        });

        // Build tree structure
        accounts.forEach(acc => {
            if (acc.ParentID && map[acc.ParentID]) {
                map[acc.ParentID].children.push(map[acc.AccountGroupID]);
            } else {
                tree.push(map[acc.AccountGroupID]);
            }
        });

        // Clean up empty children arrays
        Object.values(map).forEach(node => {
            if (node.children.length === 0) {
                delete node.children;
            }
        });

        return tree;
    };

    // INITIALIZATION CHECK - Add this to verify all functions are loaded
    console.log("=== ChartOfAccountController Function Check ===");

    // Check if all required functions exist
    const requiredFunctions = [
        'getTopParent',
        'getTopParentNameEnhanced',
        'autoSelectAccountType',
        'getAccountTypeIcon',
        'getAccountTypeColor',
        'getSelectedAccountTypeName',
        'debugSelectionEnhanced',
        'forceAutoSelectEnhanced',
        'onAccountTypeChange',
        'refreshAccountTypes',
        'findNodeInTree',
        'LoadAccountTypes',
        'GetAccountCode',
        'FillAccount',
        'proceedWithFillAccount',
        'OpenCreateAccount',
        'OpenCreateGroup',
        'resetFormData'
    ];

    const missingFunctions = [];
    requiredFunctions.forEach(funcName => {
        if (typeof $scope[funcName] !== 'function') {
            missingFunctions.push(funcName);
            console.error(`❌ Missing function: $scope.${funcName}`);
        } else {
            console.log(`✅ Function available: $scope.${funcName}`);
        }
    });

    if (missingFunctions.length > 0) {
        console.error("❌ MISSING FUNCTIONS:", missingFunctions);
        alert("Some required functions are missing. Please check the console for details.");
    } else {
        console.log("✅ All required functions are loaded successfully!");
    }

    // Initialize the controller after function check
    $timeout(() => {
        console.log("Initializing Chart of Accounts...");
        $scope.LoadAccountTypes();
        $scope.GetChartOfAccounts();
        console.log('Chart of Accounts initialization complete');
    }, 100);

}]); // End of ChartOfAccountController