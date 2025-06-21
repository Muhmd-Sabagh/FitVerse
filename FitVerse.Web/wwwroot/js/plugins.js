// This file is intended for various jQuery plugins or custom JavaScript
// functionalities that extend jQuery.

// Example: A simple custom jQuery plugin for fading elements
(function ($) {
    $.fn.extend({
        fadeAndRemove: function (speed, callback) {
            return this.animate({ opacity: 0 }, speed, function () {
                $(this).remove();
                if (typeof callback === 'function') {
                    callback.call(this);
                }
            });
        },
        // Another example: a simple logger
        logElements: function (message = "Elements found:") {
            console.log(message, this);
            return this; // Enable chaining
        }
    });
})(jQuery);

// You would add other plugin code here.
// For instance, if you were using a light-box plugin, its code would go here.
// Or if you had custom form handling scripts that aren't specific to a single page.

// Example of a non-jQuery related utility, if this file also serves for general JS
function debounce(func, delay) {
    let timeout;
    return function (...args) {
        const context = this;
        clearTimeout(timeout);
        timeout = setTimeout(() => func.apply(context, args), delay);
    };
}

// Example usage of the debounce function (not tied to jQuery directly, but a common utility)
// const myDebouncedFunction = debounce(() => {
//     console.log('Function debounced!');
// }, 250);
// $(window).on('resize', myDebouncedFunction);

// Ensure any initialization code for these plugins runs when the document is ready
$(document).ready(function () {
    // Example: Initialize a hypothetical "tooltip" plugin
    // $('.my-tooltip-target').tooltipPlugin();

    // Example: Apply fadeAndRemove to some elements after an action
    // $('#someButton').on('click', function() {
    //     $('.item-to-remove').fadeAndRemove(500, function() {
    //         console.log('Item removed!');
    //     });
    // });

    console.log("plugins.js loaded and ready.");
});
