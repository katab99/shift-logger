#!/bin/bash

DB_NAME="shiftLogs.db"

# Remove old database if exists
rm -f $DB_NAME

# Create and populate database
sqlite3 $DB_NAME <<EOF
CREATE TABLE shiftLogs (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    date TEXT NOT NULL,
    startTime TEXT NOT NULL,
    endTime TEXT NOT NULL,
    numOfOrders INTEGER NOT NULL,
    earnings REAL NOT NULL,
    bonus REAL NOT NULL,
    distance REAL NOT NULL
);

INSERT INTO shiftLogs (date, startTime, endTime, numOfOrders, earnings, bonus, distance) VALUES 
    ('2025-10-31', '15:10:00', '17:20:00', 9, 250.23, 0.00, 12.3),
    ('2025-10-30', '17:10:00', '19:19:00', 12, 420.22, 10.00, 20.2);

SELECT 'Database created successfully!' AS message;
SELECT * FROM shiftLogs;
EOF

echo "Done! Database saved as $DB_NAME"