
importScripts('https://www.gstatic.com/firebasejs/9.9.3/firebase-app-compat.js');
importScripts('https://www.gstatic.com/firebasejs/9.9.3/firebase-messaging-compat.js');

const firebaseConfig = {
  apiKey: "AIzaSyCCnkcbNFIKZ2MlCMEmu5vLgVnb0wyQGNE",
  authDomain: "relativeswebpush.firebaseapp.com",
  projectId: "relativeswebpush",
  storageBucket: "relativeswebpush.appspot.com",
  messagingSenderId: "540414732158",
  appId: "1:540414732158:web:224b4ba6c92dba7a97752f"
};

// Initialize Firebase
firebase.initializeApp(firebaseConfig);

const messaging = firebase.messaging();

messaging.onBackgroundMessage(function(payload) {
  console.log('[firebase-messaging-sw.js] Received background message ', payload);
  // Customize notification here
  const notificationTitle = 'Background Message Title';
  const notificationOptions = {
    body: 'Background Message body.',
    icon: '/firebase-logo.png'
  };

  self.registration.showNotification(notificationTitle,
    notificationOptions);
});