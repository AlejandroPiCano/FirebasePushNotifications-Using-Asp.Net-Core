  var messaging =firebase.messaging();


  window.onload=function(){
    Notification.requestPermission()
        .then(function(){
            console.log("accept the notification");           
            return messaging.getToken({vapidKey: "XXX"});
        }).then(function(token){
            console.log(token);
            document.getElementById("token").innerHTML = token;
            alert(token);            
        }).catch(function(err){
            console.log('error requesting Permission');            
        });
    };
	
    let enableForegroundNotification=true;
    messaging.onMessage(function(payload){
        console.log("message received");
        if(enableForegroundNotification){
            const {title, ...options}=JSON.parse(payload.data.notification);
            navigator.serviceWorker.getRegistrations().then( registration =>{
                registration[0].showNotification(title, options);
            });
        }
    });

    
