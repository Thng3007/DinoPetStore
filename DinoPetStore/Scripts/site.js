function ToastNotify(content, type) {
    $.toast({
        heading: 'Thông báo',
        text: `${content}`,
        icon: `${type}`,
    })
}