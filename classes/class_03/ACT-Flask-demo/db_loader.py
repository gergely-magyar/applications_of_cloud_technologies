import sqlite3


class DBLoader():
    def __init__(self):
        self.connection = sqlite3.connect('test.db')
        self.cursor = self.connection.cursor()

        try:
            self.create_table()
        except:
            pass

    def create_table(self):
        sql = ("CREATE TABLE Meals ("
               "meal_id int, "
               "name varchar(50), "
               "price float, "
               "PRIMARY KEY (meal_id));")
        self.cursor.execute(sql)

    def get_available_id(self):
        sql = "SELECT max(meal_id) FROM Meals"
        self.cursor.execute(sql)
        available_id = self.cursor.fetchone()
        return int(available_id[0]) + 1

    def add_meal(self, name, price):
        sql = "INSERT INTO Meals (meal_id, name, price) VALUES (?, ?, ?)"
        meal_id = self.get_available_id()
        self.cursor.execute(sql, (meal_id, name, price))
        self.connection.commit()
