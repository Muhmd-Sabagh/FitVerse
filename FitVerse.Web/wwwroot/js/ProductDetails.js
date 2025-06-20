function incrementQuantity() {
    const input = document.getElementById('quantityInput');
    const max = parseInt(input.max);
    let value = parseInt(input.value);
    if (value < max) {
        input.value = value + 1;
    }
}

function decrementQuantity() {
    const input = document.getElementById('quantityInput');
    let value = parseInt(input.value);
    if (value > 1) {
        input.value = value - 1;
    }
}

// Validate quantity input
document.getElementById('quantityInput').addEventListener('change', function () {
    let value = parseInt(this.value);
    const max = parseInt(this.max);
    const min = parseInt(this.min);

    if (isNaN(value) || value < min) {
        this.value = min;
    } else if (value > max) {
        this.value = max;
    }
});