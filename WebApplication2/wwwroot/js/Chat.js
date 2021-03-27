var connection = new signalR.HubConnectionBuilder().withUrl("/Chat").build();

$(document).ready(() => {

   
    var sendMessge = $('#SendMessage')
    var Messge = $('#Message')
    var Chat = $('#Chat')
    connection.start().then(function () {
    
        //connection.invoke("SendMessage", "wahaj").catch(function (err) {
        //    return console.error(err.toString());
        //});
 
       
            $.ajax(
                {
                    method: 'POST',
                    url: '/Home/GetInbox',
                    success: resp => {

                        if (resp.status) {
                            console.log(resp.data)
                            resp.data.forEach(x => {
                                CreateInbox(x)

                            })
                            //CreateInbox(resp.data)
                        } else {
                            Alert("Some thing went wrong");
                        }


                    },
                    error: err => { console.log(err) }
                })

   
    }).catch(function (err) {
        return console.error(err.toString());
    });
   
})
connection.on("RecieveMessage", function (message) {

    CreateInbox(message)
    var objDiv = document.getElementById("Chat");
    objDiv.scrollTop = objDiv.scrollHeight;
});

function CreateInbox(message)
{

    var view;
    var date = message.createdDate.toString().split('T').slice(0, 3)[1].split('.')[0].slice(0, 5);
    //date = date.split('-').slice(0, 3)
    //date = `${date[2]}-${date[1]}-${date[0]}`

    if (message.senderId == parseInt(localStorage.getItem("UserId"))) {
        view = `<div class="chat-message-right pb-4">
                                    <div>
                                        <img src="/images/${message.userImage}" class="rounded-circle mr-1" alt="Chris Wood" width="40" height="40">
                                        <div class="text-muted small text-nowrap mt-2">${date}</div>
                                    </div>
                                    <div class="flex-shrink-1 bg-light rounded py-2 px-3 mr-3">
                                        <div class="font-weight-bold mb-1">You</div>
                                       ${message.message}
                                    </div>
                                </div>`;
    }
    else {
        view = `<div class="chat-message-left pb-4">
                                    <div>
                                        <img src="/images/${message.userImage}" class="rounded-circle mr-1" alt="Sharon Lessman" width="40" height="40">
                                        <div class="text-muted small text-nowrap mt-2">${date}</div>
                                    </div>
                                    <div class="flex-shrink-1 bg-light rounded py-2 px-3 ml-3">
                                        <div class="font-weight-bold mb-1">${message.senderName}</div>
                                       ${message.message}
                                    </div>
                                </div>`;
    }
    $('#Chat').append(view)
    $('#Message').val('')
}



$('#SendMessage').on('click', (event) => {
    event.preventDefault();
    var data = {
        "Message": $('#Message').val(),
        "SenderId": parseInt(localStorage.getItem("UserId")),
        "Chatid": 1
    };
    connection.invoke("SendMessage", data).catch(function (err) {
        return console.error(err.toString());
    });
   
});




