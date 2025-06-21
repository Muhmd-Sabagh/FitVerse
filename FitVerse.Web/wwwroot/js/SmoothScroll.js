/*
 * SmoothScroll.js: Custom script for smooth scrolling to anchor links.
 * This provides a more fluid navigation experience.
 */

(function () {
    'use strict';

    // Check for browser support for scroll-behavior property (CSS solution preferred if supported)
    // If not supported, or if you want custom easing/offset, use JS.
    // Modern browsers support `scroll-behavior: smooth;` in CSS on `html` or `body`.
    // If you're using `scroll-behavior: smooth;`, this JS might be redundant for simple cases.

    // Polyfill for Element.scrollIntoView if needed (for options like 'smooth')
    // Not explicitly providing a full polyfill here, as modern browsers mostly support it.
    // For older browsers, a library like `smoothscroll-polyfill` might be necessary.

    document.addEventListener('DOMContentLoaded', function () {
        // Select all links with hashes and exclude internal Bootstrap tabs/carousels
        const anchorLinks = document.querySelectorAll('a[href^="#"]:not([data-bs-toggle="collapse"]):not([data-bs-target^="#"]):not([data-bs-slide])');

        anchorLinks.forEach(link => {
            link.addEventListener('click', function (e) {
                // Prevent default jump behavior
                e.preventDefault();

                // Get the target element by its ID
                const targetId = this.getAttribute('href').substring(1); // Remove the '#'
                const targetElement = document.getElementById(targetId);

                if (targetElement) {
                    // Calculate offset for fixed headers/navbars
                    const headerOffset = 70; // Adjust this value to the height of your fixed header/navbar
                    const elementPosition = targetElement.getBoundingClientRect().top;
                    const offsetPosition = elementPosition + window.pageYOffset - headerOffset;

                    window.scrollTo({
                        top: offsetPosition,
                        behavior: "smooth"
                    });

                    // Update URL hash without jumping (optional, useful for back button)
                    if (history.pushState) {
                        history.pushState(null, null, this.getAttribute('href'));
                    } else {
                        window.location.hash = this.getAttribute('href');
                    }
                }
            });
        });
    });

})();

