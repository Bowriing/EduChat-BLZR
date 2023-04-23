function saveAsFile(filename, base64Content, contentType) {
    const link = document.createElement("a");
    link.href = "data:" + contentType + ";base64," + base64Content;
    link.download = filename;
    link.click();
    link.remove();
}
