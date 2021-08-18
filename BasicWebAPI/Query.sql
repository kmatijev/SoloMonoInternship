DROP TABLE STUDENT;
DROP TABLE GRADE;

CREATE TABLE Grade(
GradeID INTEGER PRIMARY KEY,
GradeName VARCHAR(15)
);

CREATE TABLE Student(
StudentID INTEGER PRIMARY KEY,
StudentName VARCHAR(15),
GradeID INTEGER FOREIGN KEY REFERENCES Grade(GradeID)
);


INSERT INTO GRADE(GradeID, GradeName)
VALUES (1, 'Catterpillars');
INSERT INTO GRADE(GradeID, GradeName)
VALUES (2,'Butterflys');

INSERT INTO STUDENT(StudentID, StudentName, GradeID)
VALUES (1, 'Marko', 1);
INSERT INTO STUDENT(StudentID, StudentName, GradeID)
VALUES (2, 'Darko', 1);
INSERT INTO STUDENT(StudentID, StudentName, GradeID)
VALUES (3, 'Parko', 1);
INSERT INTO STUDENT(StudentID, StudentName, GradeID)
VALUES (4, 'Karko', 2);
INSERT INTO STUDENT(StudentID, StudentName, GradeID)
VALUES (5, 'Larko', 2);
INSERT INTO STUDENT(StudentID, StudentName, GradeID)
VALUES (6, 'Sarko', 2);

