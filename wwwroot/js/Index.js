
$('#swaggerBtn').click(function () {
    window.location.href = '/swagger';
});

$('#menu-homeTab').click(function () {
    $('#dropdownMenu').hide();
});
$('#menu-docTab').click(function () {
    $('#dropdownMenu').toggle();
});

$('#menu-apiEndp').click(function () {
    $('#menu-docTab, #tabDoc').removeClass('active')
    //$('#tabDoc').removeClass('show, active')

});

