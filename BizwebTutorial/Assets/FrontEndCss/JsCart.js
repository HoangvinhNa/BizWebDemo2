
$(document).ready(function () {
    var ProductId = $('.box_product_index .item_select').attr('data-id');
    $('.area_btn[data-id="' + ProductId + '"] .add_to_cart').click(function () {
        var _itemproductselect = $('.box_product_index .item_select[data-id="' + ProductId + '"]');
        var productImage = _itemproductselect.attr('data-image');
        var productname = _itemproductselect.attr('data-name');
        var productprice = _itemproductselect.attr('data-price');
        $('.area_btn #ProductId').val(ProductId);
        $('.area_btn #ProductImage').val(productImage);
        $('.area_btn #ProductName').val(productname);
        $('.area_btn #ProductPrice').val(productprice);
        $('.area_btn #ProductQuantity').val(1);
    });
});
