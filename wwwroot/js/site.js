$(document).ready(function(){
    $('.switch-btn_img').click(function(){
        $(this).toggleClass('active');
        let parrent = $(this).closest('.street_holder');
        parrent.find('.house-item_holder').slideToggle();
    })

    $('.street-item_checkbox').click(function () {
        let id = $(this).data('id');
        let items = $(`.item_block[data-streetid="${id}"]`);
        let checked = $(this).prop('checked')
        for (let i = 0; i < items.length; i++) {
            if (checked) {
                $(items[i]).show();
            } else {
                $(items[i]).hide();
            }
        }

        let parrent = $(this).closest('.street_holder');
        parrent.find('.house-item_checkbox').prop("disabled", !this.checked);
        if (!checked) {
            parrent.find('.house-item_checkbox').removeAttr("checked");
        } else {
            parrent.find('.house-item_checkbox').attr("checked", "checked");
        }
        
    })

    $('.house-item_checkbox').click(function () {
        let id = $(this).data('id');
        let items = $(`.item_block[data-houseid="${id}"]`);
        let checked = $(this).prop('checked')
        for (let i = 0; i < items.length; i++) {
            if (checked) {
                $(items[i]).show();
            } else {
                $(items[i]).hide();
            }
        }
    })
    $('#pop-up-btn').click(function () {
        $('#pop-up-block').fadeToggle();
    })
    $('#pop-up-closebtn').click(function () {
        $('#pop-up-block').fadeToggle();
    })
    $('#createStreetForm').submit(function (e) {
        e.preventDefault();

        var streetName = $('#streetName').val();

        $.ajax({
            url: '/Adresses/CreateStreet',
            type: 'POST',
            data: { name: streetName },
            success: function (response) {
                if (response && response.id && response.name) {
                    var newOption = new Option(response.name, response.id, false, false);
                    $('#streetSelect').append(newOption);
                } else {
                    alert("Ошибка при добавлении улицы.");
                }
                $('#pop-up-block').fadeToggle();
            },
            error: function () {
                alert("Произошла ошибка на сервере.");
            }
        });
    })
})