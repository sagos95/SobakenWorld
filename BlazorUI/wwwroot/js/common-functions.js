

function saveAsFile(filename, bytesBase64) {
    let link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
};

function getImageSize(elementId) {
    const el = document.getElementById(elementId);
    const size = {
        "width": el.naturalWidth,
        "height": el.naturalHeight
    };
    return size;
}