// site.js - Custom JavaScript for FitVerse.Web application

// This script runs when the DOM is fully loaded.
document.addEventListener('DOMContentLoaded', function () {
    console.log('FitVerse site.js loaded.');

    // Example: Add a dynamic year to the footer copyright
    const currentYearElement = document.querySelector('footer p.mb-0');
    if (currentYearElement) {
        const currentYear = new Date().getFullYear();
        // Assuming the copyright text includes a placeholder for the year
        // e.g., "© Copyright [YEAR] FitVerse. All rights reserved."
        currentYearElement.innerHTML = currentYearElement.innerHTML.replace('@DateTime.Now.Year', currentYear.toString());
        // Or if it's simpler:
        // currentYearElement.textContent = `© Copyright ${currentYear} FitVerse. All rights reserved.`;
    }

    // Example: Simple "scroll to top" button functionality
    const scrollToTopBtn = document.createElement('button');
    scrollToTopBtn.innerHTML = '<i class="fas fa-arrow-up"></i>';
    scrollToTopBtn.classList.add('scroll-to-top-btn', 'btn', 'btn-primary');
    document.body.appendChild(scrollToTopBtn);

    // Style for the scroll-to-top button (add this to your site.css or here if inline)
    const style = document.createElement('style');
    style.textContent = `
        .scroll-to-top-btn {
            position: fixed;
            bottom: 20px;
            right: 20px;
            width: 50px;
            height: 50px;
            border-radius: 50%;
            background-color: #3a86ff;
            color: white;
            border: none;
            box-shadow: 0 4px 10px rgba(0,0,0,0.2);
            display: flex;
            align-items: center;
            justify-content: center;
            cursor: pointer;
            opacity: 0;
            visibility: hidden;
            transition: opacity 0.3s ease, visibility 0.3s ease, transform 0.3s ease;
            z-index: 1000;
        }
        .scroll-to-top-btn.show {
            opacity: 1;
            visibility: visible;
            transform: translateY(-5px);
        }
        .scroll-to-top-btn:hover {
            background-color: #2a6ae0;
            transform: translateY(-8px);
        }
    `;
    document.head.appendChild(style);

    window.addEventListener('scroll', function () {
        if (window.scrollY > 300) { // Show button after scrolling 300px
            scrollToTopBtn.classList.add('show');
        } else {
            scrollToTopBtn.classList.remove('show');
        }
    });

    scrollToTopBtn.addEventListener('click', function () {
        window.scrollTo({
            top: 0,
            behavior: 'smooth'
        });
    });

    // Add any other global site-specific JavaScript here
    // For example, handling dynamic content, form submissions (if not handled by ASP.NET Core MVC's unobtrusive validation),
    // or interactive elements.

    // Example of a simple message display function (replaces `alert()`)
    // This is a basic version, for more complex apps, use a dedicated modal library (like Bootstrap's modal).
    window.displayMessage = function (title, message) {
        // Find Bootstrap's modal elements if available, or create simple ones
        let modalElement = document.getElementById('globalMessageModal');
        if (!modalElement) {
            modalElement = document.createElement('div');
            modalElement.id = 'globalMessageModal';
            modalElement.classList.add('modal', 'fade');
            modalElement.setAttribute('tabindex', '-1');
            modalElement.setAttribute('aria-labelledby', 'globalMessageModalLabel');
            modalElement.setAttribute('aria-hidden', 'true');
            modalElement.innerHTML = `
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="globalMessageModalLabel"></h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body"></div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            `;
            document.body.appendChild(modalElement);
        }

        const modalTitle = modalElement.querySelector('.modal-title');
        const modalBody = modalElement.querySelector('.modal-body');

        if (modalTitle) modalTitle.textContent = title;
        if (modalBody) modalBody.textContent = message;

        const bootstrapModal = new bootstrap.Modal(modalElement);
        bootstrapModal.show();
    };

    // Example usage:
    // window.displayMessage('Success!', 'Your operation was completed successfully.');
    // window.displayMessage('Error!', 'Something went wrong. Please try again.');
});
