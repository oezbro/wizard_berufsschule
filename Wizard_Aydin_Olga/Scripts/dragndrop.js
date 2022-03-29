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
        draggedElementParent.classList.add("noTurn");
        ownButton = draggedElementParent.querySelector(".turnOver");
        ownButton.classList.add("active");
        ownButton.addEventListener("click", function () {
            document.querySelector(".turn").classList.remove("turn");
            document.querySelector(".noTurn").classList.remove("noTurn");
            ownButton.classList.remove("active");
        })
    } else {
        if (draggedElementParent.querySelector(".opponentCardWrapper").children.length > 0) {
            draggedElementParent.querySelector(".startEvaluation").classList.add("active");
        } else {
            draggedElementParent.querySelector(".startEvaluationFinal").classList.add("active");
        }
    }
}

$("#startEvaluationFinal").click(function () {
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
    button = document.querySelector(".confirmationTricks");
    let buttParent = button.parentNode.parentNode;
    button.onclick = function () {
    tricksAmount = document.querySelector(".trickamountContainer .trick input:checked").getAttribute("value");
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

// Evaluation helper
roundNumber = document.querySelector(".playingGround").getAttribute("data-round");
ownTricks = 0;
opponentTricks = 0;
trumpColor = document.querySelector(".trumpDisplay > div").classList[0];
ownName = document.querySelector(".playerOneName").textContent;
opponentName = document.querySelector(".playerTwoName").textContent;
document.getElementById("startEvaluation").onclick = function () {
    i = 0;
    document.querySelectorAll(".playingBoard .card").forEach(card => {
        if (i == 0) {
            cardColor = card.getAttribute("data-color");
            cardNumber = card.getAttribute("data-value");
        } else {
            if (card.getAttribute("data-color") == cardColor) {
                if (card.getAttribute("data-value") > cardNumber) {
                    opponentTricks = (opponentTricks + 1);
                    changeText(opponentName);
                } else {
                    ownTricks = (ownTricks + 1);
                    changeText(ownName);
                }
            } else if (card.getAttribute("data-color") != cardColor) {
                if (cardColor.includes(trumpColor) && !card.getAttribute("data-color").includes(trumpColor) && card.getAttribute("data-color") != 15) {
                    ownTricks = (ownTricks + 1);
                    changeText(ownName);
                } else if (!cardColor.includes(trumpColor) && card.getAttribute("data-color").includes(trumpColor) && cardNumber != 15) {
                    opponentTricks = (opponentTricks + 1);
                    changeText(opponentName);
                } else if (cardColor.includes(trumpColor) && card.getAttribute("data-color").includes(trumpColor) && cardNumber != 15) {
                    if (card.getAttribute("data-value") > cardNumber) {
                        opponentTricks = (opponentTricks + 1);
                        changeText(opponentName);
                    } else {
                        ownTricks = (ownTricks + 1);
                        changeText(ownName);
                    }
                } else {
                    ownTricks = (ownTricks + 1);
                    changeText(ownName);
                }
            } else if ((cardNumber == 14 && card.getAttribute("data-value") != 14) || (cardNumber != 15 && card.getAttribute("data-value") == 15)) {
                opponentTricks = (opponentTricks + 1);
                changeText(opponentName);
            } else if ((cardNumber != 14 && card.getAttribute("data-value") == 14) || (cardNumber == 14 && card.getAttribute("data-value") == 14) || (cardNumber == 15 && card.getAttribute("data-value") != 15) || (cardNumber == 15 && card.getAttribute("data-value") == 15)) {
                ownTricks = (ownTricks + 1);
                changeText(ownName);
            }
        }
        i++;
    });
    startNewRound()
}
function changeText(name) {
    document.querySelector(".lightboxEvaluation").style.opacity = 1;
    document.querySelector(".lightboxEvaluation").innerHTML = '<p>' + name + ' hat den Stich gemacht!</p>' ;
}

function startNewRound() {
    document.querySelector(".playingBoard").querySelectorAll(".card").forEach(card => {
        card.remove();
    })
    document.querySelector(".ownCardDeck").style.pointerEvents = 'all';
    document.querySelector(".ownCardDeck").classList.remove("noTurn");
    document.querySelector(".ownCardDeck").classList.add("turn");
    document.querySelector(".opponentsCardDeck").querySelector(".active").classList.remove("active");

}