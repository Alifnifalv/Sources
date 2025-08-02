app.directive('menuItem', function () {
    return {
        restrict: 'E',
        scope: {
            item: '=',
            // fireEventFn: '&' // If you pass FireEvent via attribute binding
        },
        template: `
<div class="kt-menu-item kt-menu-item-accordion"
     data-kt-menu-item-toggle="accordion"
     data-kt-menu-item-trigger="click"
     ng-click="handleItemClick($event, item)"> <!-- NG-CLICK MOVED TO THE ROOT ELEMENT -->

    <!-- The kt-menu-link is now primarily for layout and visual grouping -->
    <div class="kt-menu-link flex items-center grow border border-transparent gap-[10px] ps-[10px] pe-[10px] py-[6px]">
        
        <span class="kt-menu-icon w-[20px] items-start text-muted-foreground">
            <img ng-if="item.IconImage && (item.IconImage.endsWith('.png') || item.IconImage.endsWith('.jpg') || item.IconImage.endsWith('.jpeg') || item.IconImage.endsWith('.svg'))"
                 ng-src="{{item.IconImage}}"
                 class="w-[20px] h-[20px]"
                 alt="{{item.Title}}" />
            <i ng-if="item.IconImage && !(item.IconImage.endsWith('.png') || item.IconImage.endsWith('.jpg') || item.IconImage.endsWith('.jpeg') || item.IconImage.endsWith('.svg'))"
               class="{{item.IconImage}} text-lg"></i>
        </span>

        <span class="kt-menu-title text-sm font-medium text-foreground">
            {{item.Title}}
        </span>

        <span class="kt-menu-actions ms-auto flex items-center gap-2 ps-2" 
              ng-if="item.HtmlAttributes1 || item.HtmlAttributes2">
            
            <a href="#" ng-if="item.HtmlAttributes2" 
               title="Open in new tab" 
               ng-click="handleIconClick($event, item.HtmlAttributes2, item.Parameters, item, true)"
               class="kt-menu-action-icon">
                <i class="fa fa-folder iconlink zoomin"></i>
            </a>

            <a href="#" ng-if="item.HtmlAttributes1"
               title="Create"
               ng-click="handleIconClick($event, item.HtmlAttributes1, item.Parameters, item, false)"
               class="kt-menu-action-icon">
                <i class="fa fa-plus-circle iconlink zoomin" aria-hidden="true"></i>
            </a>
        </span>

        <span class="kt-menu-arrow text-muted-foreground w-[20px] ms-1 me-[-10px]" ng-if="item.SubItems && item.SubItems.length">
            <span class="inline-flex kt-menu-item-show:hidden">
                <i class="ki-filled ki-plus text-[11px]"></i>
            </span>
            <span class="hidden kt-menu-item-show:inline-flex">
                <i class="ki-filled ki-minus text-[11px]"></i>
            </span>
        </span>
    </div>

    <div class="kt-menu-accordion gap-1 ps-[10px] relative before:absolute before:start-[20px] before:top-0 before:bottom-0 before:border-s before:border-border"
         ng-if="item.SubItems && item.SubItems.length">
        <menu-item ng-repeat="child in item.SubItems" item="child"></menu-item>
    </div>
</div>
`,
        link: function (scope, element, attrs) {
            // Ensure FireEvent is accessible. This example assumes it's on $parent.
            // If passed as an attribute (e.g., fireEventFn: '&'), you'd use scope.fireEventFn()
            const fireEvent = scope.$parent.FireEvent;

            // This function is called when div.kt-menu-item (the root) is clicked
            scope.handleItemClick = function ($event, menuItem) {
                // The ng-click handlers for .kt-menu-actions (handleIconClick)
                // should use $event.stopPropagation(). If they do, this function
                // will not be triggered by a click on those specific action icons.

                // Call FireEvent for the main item action.
                // This will be called regardless of menuItem.HtmlAttributes being truthy or falsy,
                // similar to the old behavior where FireEvent was always called from the li's ng-click.
                if (fireEvent) {
                    fireEvent($event, menuItem.HtmlAttributes, menuItem.Parameters, menuItem);
                }

                // DO NOT stop propagation here ($event.stopPropagation()).
                // This allows the click event to be processed by KeenThemes' JavaScript
                // for accordion toggling, as data-kt-menu-item-trigger="click" 
                // is on this same element (div.kt-menu-item).
            };

            scope.handleIconClick = function ($event, htmlAttributes, parameters, menuItem, isOpenInNewTabAction) {
                $event.stopPropagation(); // CRITICAL: Prevents handleItemClick and KT accordion toggle

                if (htmlAttributes && fireEvent) {
                    fireEvent($event, htmlAttributes, parameters, menuItem);
                } else if (htmlAttributes && isOpenInNewTabAction) {
                    // Fallback or specific logic if FireEvent is not used/suitable for "open in new tab"
                    // For example, if htmlAttributes is just a URL string:
                    // if (typeof htmlAttributes === 'string' && (htmlAttributes.startsWith('http') || htmlAttributes.startsWith('/'))) {
                    //    window.open(htmlAttributes, '_blank');
                    // } else {
                    console.warn("Open in new tab: FireEvent not found or htmlAttributes not suitable for FireEvent. HtmlAttributes:", htmlAttributes);
                    // }
                }
            };
        }
    };
});