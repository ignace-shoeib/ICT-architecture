/*
BRONNEN:
https://stackoverflow.com/questions/60367733/basic-js-question-about-aws-lambda-rds-dynamodb-query-in-the-same-function
https://www.w3schools.com/sql/sql_update.asp
*/

console.log('Loading function');
const aws = require('aws-sdk');
const s3 = new aws.S3({ apiVersion: '2006-03-01' });
var mysql = require('mysql');
var connection = mysql.createConnection({
  host: 'project-db.cd4zrs9jaq6a.us-east-1.rds.amazonaws.com',
  user: 'admin',
  password: 'rootrootroot',
  database: 'Project',
});
exports.handler = async (event, context) => {
    //console.log('Received event:', JSON.stringify(event, null, 2));

    // Get the object from the event and show its content type
    const bucket = event.Records[0].s3.bucket.name;
    const key = decodeURIComponent(event.Records[0].s3.object.key.replace(/\+/g, ' '));
    const params = {
        Bucket: bucket,
        Key: key,
    };
    try {
        const { ETag } = await s3.getObject(params).promise();
        console.log('ETag:', ETag);
        var ETagString = ETag.toString().substring(1,33);
        return new Promise(function(resolve, reject) {
          connection.query("UPDATE Files SET Checksum = '"+ETagString+"' WHERE UUID = '"+key+"'", (err, result) => {
            if (err) {
              reject(err);
            } else {
              resolve(result);
            }
          });
        });
    } catch (err) {
        console.log(err);
        const message = `Error getting object ${key} from bucket ${bucket}. Make sure they exist and your bucket is in the same region as this function.`;
        console.log(message);
        throw new Error(message);
    }
};