//Class for StartView
if (document.querySelector(".startText")) {
    document.querySelector("body").classList.add("startView");
}

//Drag and drop for cards
playground = document.querySelector(".playingGround");
function onDragStart(event) {
    event.dataTransfer.setData('text/plain', event.target.id);
    draggedElement = event.path[0];
    draggedElementParent = draggedElement.parentNode.parentNode;
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
        ownButton.classList.add("active");
        ownButton.addEventListener("click", function () {
            document.querySelector(".turn").classList.remove("turn");
            document.querySelector(".noTurn").classList.remove("noTurn");
            ownButton.classList.remove("active");
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

//Remove trickcontainer
if (document.querySelector(".confirmationTricks")) {
    let button = document.querySelector(".confirmationTricks");
    let buttParent = button.parentNode.parentNode;
    button.onclick = function () {
        buttParent.remove();
        document.querySelector(".ownCardDeck").style.pointerEvents = "all";
    }
}

//Remove evaluationcontainer after 3000ms
if (document.querySelector(".lightboxEvaluation")) {
    setTimeout(function () {
        document.querySelector(".lightboxEvaluation").style.opacity = 0;
        document.querySelector(".lightboxEvaluation").style.pointerEvents = 'none';
    }, 3000);
}
