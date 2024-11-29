import sqlite3
import os

def convertToBinaryData(filename):
    # Convert digital data to binary format
    with open(filename, 'rb') as file:
        blobData = file.read()
    return blobData

def insertBLOB(empId, photo):
    try:
        sqliteConnection = sqlite3.connect("ZumGelbenBach/wwwroot/Datenbank/Produktdb.db")
        cursor = sqliteConnection.cursor()
        print("Connected to SQLite")
        sqlite_insert_blob_query = """ INSERT INTO Images
                                  (ImgID, photo) VALUES (?, ?)"""

        empPhoto = convertToBinaryData(photo)
        # Convert data into tuple format
        data_tuple = (empId, empPhoto)
        cursor.execute(sqlite_insert_blob_query, data_tuple)
        sqliteConnection.commit()
        print("Image and file inserted successfully as a BLOB into a table")
        cursor.close()

    except sqlite3.Error as error:
        print("Failed to insert blob data into sqlite table", error)
    finally:
        if sqliteConnection:
            sqliteConnection.close()
            print("the sqlite connection is closed")



image = "4.jpg"
filepath = os.getcwd() + "\\Tools\\" + image

convertToBinaryData(filepath)
insertBLOB(5, filepath)