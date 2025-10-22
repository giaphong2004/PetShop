// Ham hiển thị thông báo SweetAlert2
function showNotification(type, message) {
    const config = {
        icon: type,
        title: type === 'success' ? 'Thành công!' : 'Lỗi!',
        text: message,
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: type === 'success' ? 3000 : 4000,
        timerProgressBar: true
    };
    
    Swal.fire(config);
}

// Xử lý nút xóa với xác nhận SweetAlert2
function initDeleteButtons() {
    document.querySelectorAll('.delete-btn').forEach(function(btn) {
        btn.addEventListener('click', function(e) {
            e.preventDefault();
            const orderId = this.getAttribute('data-order-id');
            const form = this.closest('form');
            
            Swal.fire({
                title: 'Xác nhận xóa',
                text: 'Bạn có chắc chắn muốn xóa đơn hàng #' + orderId + '?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#dc3545',
                cancelButtonColor: '#6c757d',
                confirmButtonText: 'Xóa',
                cancelButtonText: 'Hủy',
                reverseButtons: true
            }).then((result) => {
                if (result.isConfirmed) {
                    form.submit();
                }
            });
        });
    });
}

// khởi tạo khi tài liệu đã sẵn sàng
document.addEventListener('DOMContentLoaded', function() {
    initDeleteButtons();
});
