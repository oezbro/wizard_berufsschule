opponentButton = document.createElement("div");
opponentButton.classList.add("startEvaluation");
opponentButton.innerHTML = "Auswertung starten";
ownButton = document.createElement("div");
ownButton.classList.add("turnOver");
ownButton.innerHTML = "Zug beenden";

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
        playground.insertBefore(ownButton, draggedElementParent);
        ownButton.addEventListener("click", function () {
        document.querySelector(".turn").classList.remove("turn");
        document.querySelector(".noTurn").classList.remove("noTurn");
        ownButton.remove();
        })
    } else {
        playground.insertBefore(opponentButton, draggedElementParent);
    }

}