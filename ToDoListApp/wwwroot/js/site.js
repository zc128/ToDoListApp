// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

if ('clipboard' in navigator) {
    let canWriteClipboard = false;
    navigator.permissions.query({ name: 'clipboard-read' }).then((perms) => {
        canWriteClipboard = perms.state;

        if (canWriteClipboard === "granted") {
            $("#copyToClipboard").on('click', () => { navigator.clipboard.writeText(window.document.getElementById('titleID').innerHTML);})
            $("#copyToClipboard").on('click', () => { $("#copyTitleSuccess").show(); })
        } else if (canWriteClipboard === "prompt") {
            $("#copyToClipboard").on('click', () => { $("#clipboardPerms").show(); })
        } else {
            return;
        }

        $("#copyToClipboard").removeAttr('hidden');
    });
}
