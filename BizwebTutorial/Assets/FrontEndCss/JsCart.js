$(document).ready(function () {
    $('.box_product_index .add_to_cart').click(function () {
        var itembutton = $(this);
        var item_parent = itembutton.parent();
        var Id = item_parent.attr('data-id');
        var NameProduct = item_parent.attr('data-name');
        var PriceProduct = item_parent.attr('data-price');
        var ImageProduct = item_parent.attr('data-image');
        var QuantityProduct = 1;
        var objects = { Id, ImageProduct,NameProduct,PriceProduct, QuantityProduct };
        var entity = JSON.stringify(objects);
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            url: '/ShopingCart/AddCart',
            data: entity,
            success: function (arh) {
                var htmlstring =
                "<div>"
                + "<img src='" + ImageProduct + "' width='50' height='50' />" + "<a class='box_cartint'>" + NameProduct + "</a>"
               + "</div>";
                $.jGrowl(htmlstring);
            },
            failure: function (response) {
             
            }
        });
    });
});
