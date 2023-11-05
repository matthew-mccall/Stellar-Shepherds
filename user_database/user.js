const { MongoClient } = require("mongodb");
const express = require("express");
const bodyParser = require("body-parser");

const app = express();
const port = 3000;

const url = "mongodb://localhost:27017/";

// Middleware to parse JSON in incoming requests
app.use(bodyParser.json());

app.post("/register", async (req, res) => {
    const { body: userInfo } = req;

    try {
        const db = await MongoClient.connect(url);
        const dbo = db.db("PlanetUser");

        // Check if the user already exists
        const existingUser = await dbo
            .collection("users")
            .findOne({ email: userInfo.email });

        if (existingUser) {
            res.status(400).json({ error: "User already exists" });
        } else {
            const userObj = getUserObj(userInfo);

            const res = await dbo.collection("users").insertOne(userObj);
            console.log(res);

            await db.close();
            res.status(201).json({ message: "User registered successfully" });
        }
    } catch (error) {
        console.error("Error registering user:", error);
        res.status(500).json({ error: "User registration failed" });
    }
});

app.post("/login", async (req, res) => {
    const { email, password } = req.body;

    try {
        const db = await MongoClient.connect(url);
        const dbo = db.db("PlanetUser");

        // Find the user in the MongoDB collection
        const user = await dbo.collection("users").findOne({ email, password });

        if (user) {
            res.status(200).json({ message: "Login successful", user });
        } else {
            res.status(401).json({ error: "Invalid credentials" });
        }

        await db.close();
    } catch (error) {
        console.error("Error finding user:", error);
        res.status(500).json({ error: "Login failed" });
    }
});

const getUserObj = ({
                        firstName,
                        lastName,
                        email,
                        profileImageUrl,
                        dateOfBirth,
                        password,
                    }) => ({
    firstName: firstName,
    lastName: lastName,
    email: email,
    profileImageUrl: profileImageUrl,
    dateOfBirth: dateOfBirth,
    password: password,
});

app.listen(port, () => {
    console.log(`Server is running on port ${port}`);
});
