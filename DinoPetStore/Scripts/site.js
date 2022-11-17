function ToastNotify(content, type) {
    $.toast({
        heading: 'Thông báo',
        text: `${content}`,
        icon: `${type}`,
    })
}

function convertJsonToDate(jsonDate) {
    var dateString = `${jsonDate}`.substr(6);
    var currentTime = new Date(parseInt(dateString));
    var month = ("0" + (currentTime.getMonth() + 1)).slice(-2);
    var day = ("0" + currentTime.getDate()).slice(-2);
    var year = currentTime.getFullYear();
    var date = day + '-' + month + '-' + year;
    return date;
}