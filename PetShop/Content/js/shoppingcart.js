
	// Đảm bảo mã chạy sau khi trang đã tải xong
    $(document).ready(function () {
        
        // Hàm gọi AJAX để cập nhật giỏ hàng
        function updateCartQuantity(productId, newQuantity) {
			var url = $('.shopping-cart-container').data('update-url');
            $.ajax({
                url: url,
                type: 'POST',
                data: {
                    id: productId,
                    quantity: newQuantity
                },
                success: function (response) {
                    // Tìm đúng card sản phẩm dựa trên data-id
                    var itemCard = $('.cart-item-card[data-id="' + productId + '"]');

                    // Cập nhật thành tiền của sản phẩm đó
                    var itemTotalPriceElement = itemCard.find('.item-total-price');
                    itemTotalPriceElement.text(response.itemTotal.toLocaleString('vi-VN') + ' ₫');

                    // Cập nhật ô input số lượng
                    var quantityInputElement = itemCard.find('.quantity-input');
                    quantityInputElement.val(response.quantity);

                    // Cập nhật tổng tiền của giỏ hàng
                    $('#cart-subtotal').text(response.cartTotal.toLocaleString('vi-VN') + ' ₫');
                    $('#cart-total').text(response.cartTotal.toLocaleString('vi-VN') + ' ₫');
                },
                error: function () {
                    alert('Đã có lỗi xảy ra, vui lòng thử lại.');
                }
            });
        }

        // Bắt sự kiện click nút TĂNG (+)
        $('.plus-btn').on('click', function () {
            var itemCard = $(this).closest('.cart-item-card');
            var productId = itemCard.data('id');
            var quantityInput = itemCard.find('.quantity-input');
            var currentQuantity = parseInt(quantityInput.val());
            var newQuantity = currentQuantity + 1;
            
            updateCartQuantity(productId, newQuantity);
        });

        // Bắt sự kiện click nút GIẢM (-)
        $('.minus-btn').on('click', function () {
            var itemCard = $(this).closest('.cart-item-card');
            var productId = itemCard.data('id');
            var quantityInput = itemCard.find('.quantity-input');
            var currentQuantity = parseInt(quantityInput.val());
            
            // Không cho giảm xuống dưới 1
            if (currentQuantity > 1) {
                var newQuantity = currentQuantity - 1;
                updateCartQuantity(productId, newQuantity);
            }
        });
    });