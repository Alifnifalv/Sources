app.controller('LayoutHeaderController', ['$scope', '$http', "$rootScope", "$window", "$timeout", function ($scope, $http, $rootScope, $window, $timeout) {
    console.log('LayoutHeader controller loaded.');

    const themeToggleButton = document.getElementById('theme-toggle');
    const lightIcon = document.querySelector('.theme-icon-light');
    const darkIcon = document.querySelector('.theme-icon-dark');
    const htmlElement = document.documentElement;


    $scope.Init = function (theme, layout, cultureCode) {
        initializeTheme();
        if (theme) {
            $scope.SelectedTheme = theme;
        }

        if (theme) {
            $scope.SelectedLayout = layout;
        }

        if (cultureCode) {
            $scope.SelectedLanguage = cultureCode;
        }
    }

    function getStoredTheme() {
        return localStorage.getItem('theme') || 'light'; // Default to light theme
    }

    // Function to set the theme in localStorage
    function setStoredTheme(theme) {
        localStorage.setItem('theme', theme);
    }

    // Function to set the theme attribute and update icons
    function setTheme(theme) {
        htmlElement.setAttribute('data-bs-theme', theme);
        updateIcons(theme);
    }

    // Function to update icons based on the theme
    function updateIcons(theme) {
        if (theme === 'light') {
            lightIcon.classList.add('active');
            darkIcon.classList.remove('active');
        } else {
            lightIcon.classList.remove('active');
            darkIcon.classList.add('active');
        }
    }

    // Initialize the theme on page load
    function initializeTheme() {
        const storedTheme = getStoredTheme();
        setTheme(storedTheme);
    }


    (() => {
        'use strict';

        // Theme management functions
        const getStoredTheme = () => localStorage.getItem('theme');
        const setStoredTheme = (theme) => localStorage.setItem('theme', theme);

        const getPreferredTheme = () => {
            const storedTheme = getStoredTheme();
            if (storedTheme) return storedTheme;

            return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
        };

        const setTheme = (theme) => {
            const effectiveTheme = theme === 'auto'
                ? window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light'
                : theme;
            document.documentElement.setAttribute('data-bs-theme', effectiveTheme);
        };

        setTheme(getPreferredTheme());

        const showActiveTheme = (theme, focus = false) => {
            const themeSwitcher = document.querySelector('#bd-theme');
            if (!themeSwitcher) return;

            const themeSwitcherText = document.querySelector('#bd-theme-text');
            const activeThemeIcon = document.querySelector('.theme-icon-active use');
            const btnToActive = document.querySelector(`[data-bs-theme-value="${theme}"]`);
            const svgOfActiveBtn = btnToActive.querySelector('svg use').getAttribute('href');

            document.querySelectorAll('[data-bs-theme-value]').forEach((element) => {
                element.classList.remove('active');
                element.setAttribute('aria-pressed', 'false');
            });

            btnToActive.classList.add('active');
            btnToActive.setAttribute('aria-pressed', 'true');
            activeThemeIcon.setAttribute('href', svgOfActiveBtn);

            const themeSwitcherLabel = `${themeSwitcherText.textContent} (${btnToActive.dataset.bsThemeValue})`;
            themeSwitcher.setAttribute('aria-label', themeSwitcherLabel);

            if (focus) themeSwitcher.focus();
        };

        window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', () => {
            const storedTheme = getStoredTheme();
            if (storedTheme !== 'light' && storedTheme !== 'dark') setTheme(getPreferredTheme());
        });

        window.addEventListener('DOMContentLoaded', () => {
            showActiveTheme(getPreferredTheme());

            document.querySelectorAll('[data-bs-theme-value]').forEach((toggle) => {
                toggle.addEventListener('click', () => {
                    const theme = toggle.getAttribute('data-bs-theme-value');
                    setStoredTheme(theme);
                    applyViewTransition(() => {
                        setTheme(theme); // Set the theme before transitioning
                        showActiveTheme(theme, true);
                    });
                });
            });
        });

        // Circular transition function
        let lastClick;
        addEventListener('click', (event) => (lastClick = event));

        function applyViewTransition(updateDOMCallback) {
            if (!document.startViewTransition) {
                // Fallback if View Transition API is unsupported
                updateDOMCallback();
                return;
            }

            // Get the click position or fallback to the center of the screen
            const x = lastClick?.clientX ?? innerWidth / 2;
            const y = lastClick?.clientY ?? innerHeight / 2;

            // Calculate the distance to the farthest corner for the circle radius
            const endRadius = Math.hypot(
                Math.max(x, innerWidth - x),
                Math.max(y, innerHeight - y)
            );

            // Create the view transition
            const transition = document.startViewTransition(() => {
                updateDOMCallback();
            });

            // Animate the transition with a circular reveal
            transition.ready.then(() => {
                document.documentElement.animate(
                    {
                        clipPath: [
                            `circle(0 at ${x}px ${y}px)`,
                            `circle(${endRadius}px at ${x}px ${y}px)`,
                        ],
                    },
                    {
                        duration: 500,
                        easing: 'ease-in-out',
                        pseudoElement: '::view-transition-new(root)', // Animate new view
                    }
                );
            });
        }

        // Dark mode toggle with transition


        // Event listener for the theme toggle button
        themeToggleButton.addEventListener('click', () => {
            const currentTheme = htmlElement.getAttribute('data-bs-theme');
            const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
            setStoredTheme(newTheme);

            applyViewTransition(() => {
                setTheme(newTheme);
            });
        });
        if (document.startViewTransition) {
            window.toggleDarkModeWithTransition = () => applyViewTransition(toggleDarkMode);
        } else {
            window.toggleDarkMode = toggleDarkMode;
        }
    })();



}]);