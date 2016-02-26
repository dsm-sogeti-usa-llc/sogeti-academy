function showLoading() {
    $('#loadingModal').modal({
        show: true,
        keyboard: false,
        backdrop: 'static'
    });
}

function hideLoading() {
    $('#loadingModal').modal({
        show: false
    });
}