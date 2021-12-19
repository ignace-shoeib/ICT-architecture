/*
BRONNEN:
https://stackoverflow.com/questions/3433465/mysql-delete-all-rows-older-than-10-minutes

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
    try {
        return new Promise(function(resolve, reject) {
          connection.query("DELETE FROM Files WHERE CreationDate < DATE_SUB(NOW(), INTERVAL 24 HOUR)", (err, result) => {
            if (err) {
              reject(err);
            } else {
              resolve(result);
            }
          });
        });
    } catch (err) {
        console.log(err);
    }
};