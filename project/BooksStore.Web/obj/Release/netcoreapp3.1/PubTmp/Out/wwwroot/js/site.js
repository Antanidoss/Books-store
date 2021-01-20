$("#addCommnet-form").hide(0);

function showAddCommentForm() {
    $("#addCommnet-form").show(200);
}

function hideAddCommentForm() {
    $("#addCommnet-form").hide(200);
}

function addPopup(title, text) {
    $(".content").html(`<div class="popup">
    <div class="popup-body">
        <div class="popub-content">
            <a asp-action="IndexBooks" asp-controller="Book" class="popup-close">X</a>
            <div class="popup-title">
                ${title}
            </div>
            <div class="popup-text">
                ${text}
            </div>
        </div>
    </div>
</div>`)
}