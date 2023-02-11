function exportFile(reportName, byteArray) {
    var link = document.createElement('a');
    link.download = reportName;
    link.href = "data:application/octet-stream;base64," + byteArray;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}       

function exportData(object) {
    console.log(object);
}

async function shareInvite() {
    try {
        await navigator.share({
            files: files,
            title: 'Avila',
            text: 'Order Receipt'
        });
        return true;
    } catch (err) {
        console.log(`Error: ${err}`);
    }
    files = [];
    return false;
}
let files = [];
function getFile(fileName, mime, format, blob) {
    var file = new File([blob], fileName, { type: `${mime}/${format}` })
    files.push(file);
    console.log(files);
}
function clearFiles() {
    files = [];
    console.log(files);
}