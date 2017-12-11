var interval;
function EmailConfirmation(email) {
    if (window.WebSocket) {
        openSocket(email, "Email");
    }
    else {
        interval = setInterval(() => {
            CheckEmailConfirmationStatus(email);
        }, 5000);
    }
}

function GameInvitationConfirmation(id) {
    if (window.WebSocket) {
        openSocket(id, "GameInvitation");
    }
    else {
        interval = setInterval(() => {
            CheckGameInvitationConfirmationStatus(id);
        }, 5000);
    }
}