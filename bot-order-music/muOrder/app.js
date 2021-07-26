const express = require('express')
const fs = require('fs');
const path = require('path');
const ytdl = require('ytdl-core');

const app = express()
const port = 3000
const baseUrl = `http://localhost:${port}`;

app.use(express.urlencoded());
app.use(express.json());

app.use(express.static('public'))

const folder = path.join(__dirname, 'musics');
app.use(express.static(folder))

app.all('*', function (req, res, next) {
    res.header("Access-Control-Allow-Origin", "*");
    res.header("Access-Control-Allow-Headers", "X-Requested-With");
    next();
});

var firebase = require("firebase");
firebase.initializeApp({
    apiKey: "AIzaSyDYyNBVskQgLGz5OYcCw7tA74h06B3-n4c",
    authDomain: "muorder-9dccf.firebaseapp.com",
    databaseURL: "https://muorder-9dccf-default-rtdb.asia-southeast1.firebasedatabase.app",
    projectId: "muorder-9dccf",
    storageBucket: "muorder-9dccf.appspot.com",
    messagingSenderId: "694013760199",
    appId: "1:694013760199:web:b19f33fa366722a0e1a4e2",
    measurementId: "G-YTBDV2C4RC"
});

var db = firebase.firestore();
var collection = db.collection("playlists");

app.get('/', (req, res) => {
    res.sendFile(__dirname + '/index.html')
})


app.post('/save-music', (req, res) => {
    try {
        console.log('rep', req.body);
        const { links } = req.body;

        if (!fs.existsSync(folder)) {
            fs.mkdirSync(folder);
        }

        const files = [];
        for (let i = 0; i < links.length; i++) {
            const file = {
                id: links[i].id,
                fileName: `${links[i].id}.mp4`,
                order: i
            };
            const filePath = path.join(folder, file.fileName);
            ytdl(links[i].link).pipe(fs.createWriteStream(filePath));
            files.push(file);
        }

        collection.doc('gQ4sEDSCBdGjVzzeenyt').set({files});

        res.send('Success!')
    } catch (error) {
        console.error(error);
    }
})

app.get('/lists', (req, res) => {
    try {
        const fileNames = [];
        var files = fs.readdirSync(folder);
        files.forEach(file => {
            console.log(file);
            fileNames.push(baseUrl + '/' + file);
        });

        console.log('fileNames', fileNames);
        res.send(fileNames)
    } catch (error) {
        console.error(error);
    }
})

app.get('/stop', (req, res) => {
    try {
        collection.doc('gQ4sEDSCBdGjVzzeenyt').set({files: []});
        res.send('Success!')
    } catch (error) {
        console.error(error);
    }
})

app.listen(port, () => {
    console.log(`Example app listening at ${baseUrl}`)
})