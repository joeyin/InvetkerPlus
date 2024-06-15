$(function () {
    var start = moment().subtract(7, 'days');
    var end = moment();
    $('input[name="daterange"]').daterangepicker({
        opens: 'right',
        startDate: start,
        endDate: end,
        maxDate: moment(),
        applyButtonClasses: 'btn-warning text-light',
        drops: "auto",
    }, function (start, end, label) {
        console.log(start, end, label)
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

    var el = document.getElementById('transaction-modal')
    var transactionModal = new bootstrap.Modal(el);

    el.addEventListener('hidden.bs.modal', function () {
        history.replaceState(null, null, ' ');
        $(this).find('form').trigger('reset');
    });

    window.onhashchange = function () {
        transactionModal.show();
    };

    if (window.location.hash === '#add') {
        transactionModal.show();
    }

    document.querySelector("form[name='add']").onsubmit = function (e) {
        e.preventDefault();
        this.classList.add('was-validated');

        if (this.checkValidity()) {
            $.ajax({
                method: "POST",
                url: "/api/transaction/add",
                data: Object.fromEntries(new FormData(e.target))
            }).done(function () {
                transactionModal.hide();
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
                transactionModal.hide();
            });
        }
    }

});