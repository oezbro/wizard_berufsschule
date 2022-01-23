function onDragStart(event) {
    event.dataTransfer.setData('text/plain', event.target.id);
    draggedElement = event.path[0];

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
}