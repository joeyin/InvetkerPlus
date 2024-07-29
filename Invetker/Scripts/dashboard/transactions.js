$(function () {
    var start = moment().subtract(7, 'days');
    var end = moment();
    $('input[name="Daterange"]').daterangepicker({
        opens: 'right',
        startDate: start,
        endDate: end,
        maxDate: moment(),
        applyButtonClasses: 'btn-warning text-light',
        drops: "auto",
    });

    $('#Datetime').daterangepicker({
        timePicker: true,
        singleDatePicker: true,
        startDate: moment(),
        maxDate: moment(),
        timePicker24Hour: false,
        autoApply: true,
        open: true,
        drops: "auto",
        applyButtonClasses: 'btn-warning text-light',
        locale: {
            format: 'YYYY/MM/DD HH:mm:ss'
        }
    })

    var transactionAddModalElement = document.getElementById('transaction-add-modal')
    var transactionAddModal = new bootstrap.Modal(transactionAddModalElement);
    transactionAddModalElement.addEventListener('hidden.bs.modal', function () {
        history.replaceState(null, null, ' ');
        $(this).find('form').trigger('reset');
    });

    window.onhashchange = function () {
        if (window.location.hash === '#add') {
            transactionAddModal.show();
        }
    };

    if (window.location.hash === '#add') {
        transactionAddModal.show();
    }

    document.querySelector("form[name='add']").onsubmit = function (e) {
        e.preventDefault();
        this.classList.add('was-validated');

        if (this.checkValidity()) {
            $.ajax({
                method: "POST",
                url: "/api/transaction",
                data: Object.fromEntries(new FormData(e.target))
            }).done(function () {
                transactionAddModal.hide();
                setTimeout(() => location.reload(), 666);
            });
        }
    }

    document.querySelector("form[name='import']").onsubmit = function (e) {
        e.preventDefault();
        this.classList.add('was-validated');

        if (this.checkValidity()) {
            $.ajax({
                method: "POST",
                url: "/api/transaction/import",
                data: Object.fromEntries(new FormData(e.target))
            }).done(function () {
                transactionAddModal.hide();
            });
        }
    }

    var transactionEditModalElement = document.getElementById('transaction-edit-modal')
    var transactionEditModal = new bootstrap.Modal(transactionEditModalElement);
    $(".transaction-delete").click(function () {
        if (window.confirm("Are you sure you want to delete the transaction ?")) {
            const id = $(this).attr("data-id");
            $.ajax({
                method: "DELETE",
                url: `/api/transaction/${id}`,
            }).done(function () {
                location.reload();
            });
        }
    })

    $(".transaction-edit").click(function () {
        const id = $(this).attr("data-id");
        $.ajax({
            method: "GET",
            url: `/api/transaction/${id}`,
        }).done(function (res) {
            $("form[name='edit'] input[name='Ticker']").val(res.Ticker);
            $("form[name='edit'] input[name='Quantity']").val(res.Quantity);
            $("form[name='edit'] select[name='Action']").val(res.Action);
            $("form[name='edit'] input[name='Price']").val(res.Price);
            $("form[name='edit'] input[name='Fee']").val(res.Fee);
            $("form[name='edit'] input[name='Datetime']").val(res.Datetime);
            $("form[name='edit']").attr("action", `/api/transaction/${id}`);
            transactionEditModal.show();
        });
    })

    document.querySelector("form[name='edit']").onsubmit = function (e) {
        e.preventDefault();
        this.classList.add('was-validated');

        if (this.checkValidity()) {
            $.ajax({
                method: "PUT",
                url: $(this).attr("action"),
                data: Object.fromEntries(new FormData(e.target))
            }).done(function () {
                transactionEditModal.hide();
                location.reload();
            });
        }
    }


    $.ajax({
        url: '/api/stock/list',
        type: 'GET',
        dataType: 'json',
        success: function (res) {
            $(".Ticker").selectize({
                plugins: ["restore_on_backspace", "clear_button"],
                delimiter: " - ",
                persist: false,
                maxItems: 1,
                valueField: "Id",
                labelField: "Symbol",
                searchField: ['Name', 'Symbol'],
                options: res,
            });

        }
    });


});