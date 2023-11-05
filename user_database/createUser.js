const {MongoClient} = require("mongodb");
const url = "mongodb://localhost:27017/";

async function createUser(req){
    const {body: userInfo} = req

    // Creating database
    const db  = await MongoClient.connect(url)
    const dbo = db.db("PlanetUser");
    const userObj = getUserObj(userInfo);

    const res = await dbo.collection("users").insertOne(userObj);
    console.log(res);
    const result = await dbo.collection("users").find({}).toArray()
    console.log(result)
    await db.close();
}

const getUserObj = ({
                        firstName,
                        lastName,
                        email,
                        profileImageUrl,
                        dateOfBirth,

                    }) => ({
    firstName: firstName,
    lastName: lastName,
    email: email,
    profileImageUrl: profileImageUrl,
    dateOfBirth: dateOfBirth,

});
//
// (async () => await createUser(
//     {body: {firstName: "staria 2"}}
// ))();

module.exports = {
    createUser
}

