// ===== QUẢN LÝ SẢN PHẨM JAVASCRIPT =====

// Hàm hiển thị thông báo
function showNotification(type, message) {
    // Xóa thông báo cũ nếu có
    const existingNotification = document.querySelector('.notification');
    if (existingNotification) {
        existingNotification.remove();
    }

    // Tạo element thông báo mới
    const notification = document.createElement('div');
    notification.className = `notification ${type}`;
    
    // Icon dựa trên loại thông báo
    let icon = '';
    if (type === 'success') {
        icon = '<i class="fas fa-check-circle"></i>';
    } else if (type === 'error') {
        icon = '<i class="fas fa-exclamation-circle"></i>';
    } else if (type === 'info') {
        icon = '<i class="fas fa-info-circle"></i>';
    }
    
    notification.innerHTML = `
        ${icon}
        <span>${message}</span>
        <button class="notification-close" onclick="this.parentElement.remove()">×</button>
    `;
    
    // Thêm vào body
    document.body.appendChild(notification);
    
    // Tự động ẩn sau 5 giây
    setTimeout(() => {
        if (notification.parentElement) {
            notification.style.animation = 'slideOut 0.3s ease';
            setTimeout(() => notification.remove(), 300);
        }
    }, 5000);
}

// Xử lý xác nhận xóa sản phẩm
document.addEventListener('DOMContentLoaded', function() {
    // Xử lý nút xóa
    const deleteButtons = document.querySelectorAll('.delete-btn');
    
    deleteButtons.forEach(button => {
        button.addEventListener('click', function(e) {
            e.preventDefault();
            
            const productId = this.getAttribute('data-product-id');
            const form = this.closest('form');
            
            // Hiển thị confirm dialog
            if (confirm('Bạn có chắc chắn muốn xóa sản phẩm này không?')) {
                form.submit();
            }
        });
    });

    // Preview ảnh khi chọn file (cho trang Create/Edit)
    const imageInput = document.getElementById('imageFile');
    const imagePreview = document.getElementById('imagePreview');
    const uploadBox = document.querySelector('.image-upload-box');
    
    if (imageInput && imagePreview && uploadBox) {
        // Tạo container cho preview và nút thay đổi
        const previewContainer = document.createElement('div');
        previewContainer.id = 'previewContainer';
        previewContainer.style.textAlign = 'center';
        previewContainer.style.display = 'none';
        
        // Tạo nút thay đổi ảnh
        const changeImageBtn = document.createElement('button');
        changeImageBtn.type = 'button';
        changeImageBtn.className = 'btn btn-outline-secondary btn-sm mt-3';
        changeImageBtn.innerHTML = '<i class="fas fa-sync-alt me-1"></i>Thay đổi ảnh';
        
        // Thêm preview và nút vào container
        imagePreview.parentNode.insertBefore(previewContainer, imagePreview.nextSibling);
        previewContainer.appendChild(imagePreview);
        previewContainer.appendChild(changeImageBtn);
        
        // Xử lý khi chọn file
        imageInput.addEventListener('change', function(e) {
            const file = e.target.files[0];
            if (file) {
                // Kiểm tra kích thước file (tối đa 5MB)
                if (file.size > 5 * 1024 * 1024) {
                    alert('Kích thước ảnh không được vượt quá 5MB!');
                    this.value = '';
                    return;
                }
                
                // Kiểm tra định dạng file
                const validTypes = ['image/jpeg', 'image/jpg', 'image/png'];
                if (!validTypes.includes(file.type)) {
                    alert('Chỉ chấp nhận file ảnh định dạng JPG, JPEG, PNG!');
                    this.value = '';
                    return;
                }
                
                const reader = new FileReader();
                reader.onload = function(e) {
                    imagePreview.src = e.target.result;
                    imagePreview.style.display = 'block';
                    
                    // Ẩn form upload, hiện preview container
                    uploadBox.style.display = 'none';
                    previewContainer.style.display = 'block';
                };
                reader.readAsDataURL(file);
            }
        });
        
        // Xử lý khi click nút thay đổi ảnh
        changeImageBtn.addEventListener('click', function() {
            imageInput.value = '';
            imagePreview.style.display = 'none';
            imagePreview.src = '#';
            uploadBox.style.display = 'block';
            previewContainer.style.display = 'none';
        });
    }

    // Xử lý validation form
    const productForm = document.getElementById('productForm');
    if (productForm) {
        productForm.addEventListener('submit', function(e) {
            const tenSP = document.querySelector('input[name="TenSP"]');
            const giaSP = document.querySelector('input[name="GiaSP"]');
            
            let isValid = true;
            let errorMessage = '';

            // Validate tên sản phẩm
            if (!tenSP || tenSP.value.trim() === '') {
                isValid = false;
                errorMessage += 'Vui lòng nhập tên sản phẩm.\n';
            }

            // Validate giá
            if (!giaSP || giaSP.value.trim() === '' || parseFloat(giaSP.value) <= 0) {
                isValid = false;
                errorMessage += 'Vui lòng nhập giá hợp lệ (lớn hơn 0).\n';
            }

            if (!isValid) {
                e.preventDefault();
                alert(errorMessage);
            }
        });
    }

    // Format số tiền khi nhập
    const priceInput = document.querySelector('input[name="GiaSP"]');
    if (priceInput) {
        priceInput.addEventListener('blur', function() {
            let value = parseFloat(this.value);
            if (!isNaN(value)) {
                this.value = value.toFixed(0);
            }
        });
    }
});

// Animation khi hover vào các hàng trong b?ng
document.addEventListener('DOMContentLoaded', function() {
    const tableRows = document.querySelectorAll('.table tbody tr');
    
    tableRows.forEach(row => {
        row.addEventListener('mouseenter', function() {
            this.style.transition = 'all 0.2s ease';
        });
    });
});

// CSS animation cho slideOut
const style = document.createElement('style');
style.textContent = `
    @keyframes slideOut {
        from {
            transform: translateX(0);
            opacity: 1;
        }
        to {
            transform: translateX(400px);
            opacity: 0;
        }
    }
`;
document.head.appendChild(style);
