playground = document.querySelector(".playingGround");

function onDragStart(event) {
    event.dataTransfer.setData('text/plain', event.target.id);
    draggedElement = event.path[0];
    draggedElementParent = draggedElement.parentNode;

}
function onDragOver(event) {
    event.preventDefault();
}

function onDrop(event) {
    const id = event.dataTransfer.getData('text');
    const draggableElement = draggedElement;
    const dropzone = document.getElementById("playingBoard");
    dropzone.appendChild(draggableElement);
    event.dataTransfer.clearData();
    if (draggedElementParent.classList.contains("turn")) {
        ownButton = draggedElementParent.querySelector(".turnOver");
        draggedElementParent.querySelector(".turnOver").classList.add("active");
        ownButton.addEventListener("click", function () {
            document.querySelector(".turn").classList.remove("turn");
            document.querySelector(".noTurn").classList.remove("noTurn");
            draggedElementParent.querySelector(".turnOver").classList.remove("active");
        })
    } else {
        draggedElementParent.querySelector(".startEvaluation").classList.add("active");
    }
}

$("#startEvaluation").click(function () {
    $.ajax({
        type: "POST",
        data: JSON.stringify(model),
        url: url,
        contentType: "application/json"
    }).done(function (res) {
    });
});