function init(maxId) {
    setInterval(function () {
        GetRecord(maxId);
    }, 30 * 1000);
}

function GetRecord(maxId) {
    $.ajax({
        url: '/Home/GetRecord',
        data: { maxId: maxId },
        success: function (records) {
            if (records && records.length > 0) {
                alert('接獲入侵通知');
                window.location.reload();
            }
        }
    });
}