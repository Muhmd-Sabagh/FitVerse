/*
 * vendor.js: JavaScript from third-party libraries, plugins, or custom UI components.
 * This file is for organizing external scripts.
 */

// Example: A simple "lazy load" script for images (could be a vendor library)
(function () {
    document.addEventListener("DOMContentLoaded", function () {
        let lazyloadImages = document.querySelectorAll("img.lazy");
        let lazyloadThrottleTimeout;

        function lazyload() {
            if (lazyloadThrottleTimeout) {
                clearTimeout(lazyloadThrottleTimeout);
            }
            lazyloadThrottleTimeout = setTimeout(function () {
                let scrollTop = window.pageYOffset;
                lazyloadImages.forEach(function (img) {
                    if (img.offsetTop < (window.innerHeight + scrollTop + 200)) { // 200px buffer
                        img.src = img.dataset.src;
                        img.classList.remove('lazy');
                    }
                });
                if (lazyloadImages.length == 0) {
                    document.removeEventListener("scroll", lazyload);
                    window.removeEventListener("resize", lazyload);
                    window.removeEventListener("orientationChange", lazyload);
                }
            }, 20);
        }

        document.addEventListener("scroll", lazyload);
        window.addEventListener("resize", lazyload);
        window.addEventListener("orientationChange", lazyload);
        lazyload(); // Initial check
    });
})();

// Example: A simple polyfill for older browsers (e.g., forEach on NodeList)
if (window.NodeList && !NodeList.prototype.forEach) {
    NodeList.prototype.forEach = Array.prototype.forEach;
}

// Add other vendor-specific JavaScript here as needed.
// For example, if you manually included a specific animation library or custom form validation plugin.

// Note: jQuery and Bootstrap JS are typically included via CDN in _Layout.cshtml,
// so they generally wouldn't be put here unless you were bundling and hosting them locally.
