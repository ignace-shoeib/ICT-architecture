import pymysql
from datetime import datetime, timedelta;

endpoint = 'kaine-db.cqftybxhj9nh.us-east-1.rds.amazonaws.com'
username = 'admin'
password = 'rootrootroot'
database_name = 'Project'

connection = pymysql.connect(host=endpoint, user=username, passwd=password, db=database_name)

past = datetime.now() - timedelta(days=1)
present = datetime.now()


def handler():
    cursor = connection.cursor()
    cursor.execute(
        """
        DELETE FROM Files
        WHERE CreationDate < DATE_SUB(NOW(), INTERVAL 24 HOUR);
        """
    )


    rows = cursor.fetchall()

    for row in rows:
        print(row)


handler()