package com.jorgebg.classprobuilder.databases

import androidx.room.*

@Dao
interface Provisional_Dao {
    //inserts para las tablase
    @Insert(onConflict = OnConflictStrategy.IGNORE)
    suspend fun addStudent(student:StudentProvisional)
    @Insert(onConflict = OnConflictStrategy.IGNORE)
    suspend fun addSubject(subject:SubjectProvisional)
    @Insert(onConflict = OnConflictStrategy.IGNORE)
    suspend fun addStudent_Subject(student_subject:Student_Subject_Provisional)
   @Insert(onConflict = OnConflictStrategy.IGNORE)
    suspend fun  addSubjectPonderation(subjectPond:subject_ponderation_provisional)
    @Insert(onConflict = OnConflictStrategy.IGNORE)
    suspend fun  addStudentPonderations(subjectPond:Student_ponderation_provisional)

    //deletes para las tablas
    @Delete
    suspend fun deleteStudent(student:StudentProvisional)
    @Delete
    suspend fun  deleteSubject(subject:SubjectProvisional)
    @Delete
    suspend fun  deleteStudent_Subject(studentSubject:Student_Subject_Provisional)
   @Delete
    suspend fun deleteSubjectPonderation(subjectPond:subject_ponderation_provisional)
    @Delete
    suspend fun  deleteStudentPonderation(studentPond:Student_ponderation_provisional)


    //actualizar las tablas
    @Update
    suspend fun updateStudent(student:StudentProvisional)
    @Update
    suspend fun  updateSubject(subject: SubjectProvisional)
    @Update
    suspend fun  updateStudentSubject(studentSubject:Student_Subject_Provisional)
    @Update
    suspend fun updateSubjectPonderations(subjectPond:subject_ponderation_provisional)


    //queries de la tabla alumno
    @Query("SELECT nombre FROM student_prov WHERE id == :user_id")
    suspend fun showName(user_id:Int):String
    @Query("SELECT apellido1 FROM student_prov WHERE id == :user_id")
    suspend fun showFirstSurname(user_id:Int):String
    @Query("SELECT apellido2 FROM student_prov WHERE id == :user_id")
    suspend fun showSecondSurname(user_id:Int):String
    @Query("SELECT id FROM student_prov ORDER BY apellido1 ASC")
    suspend fun getStudentIDs():List<Int>
    @Query("SELECT nombre FROM student_prov ORDER BY apellido1 ASC")
    suspend fun getOrderedNames():List<String>
   @Query("SELECT apellido1 FROM student_prov ORDER BY apellido1 ASC")
   suspend fun getOrderedFirsNames():List<String>
   @Query("SELECT apellido2 FROM student_prov ORDER BY apellido1 ASC")
   suspend fun getLastNames():List<String>
    @Query("SELECT * FROM student_prov ORDER BY apellido1 ASC")
    suspend fun getAllStudents():List<StudentProvisional>
    @Query("SELECT * FROM student_prov WHERE nombre==:nombre_sel")
    suspend fun getStudentComplete(nombre_sel : String):StudentProvisional

    //queries de la tabla asignatura
    @Query("SELECT nombre FROM subject_prov WHERE id == :subject_id")
    suspend fun showSubjectName(subject_id:Int):String
    @Query("SELECT partes FROM subject_prov WHERE id == :subject_id")
    suspend fun showPartitions(subject_id:Int):Int
    @Query("SELECT nombre FROM subject_prov ORDER BY nombre")
    suspend fun getSubjectNames():List<String>
    @Query("SELECT id FROM subject_prov")
    suspend fun getSubjectIDs():List<Int>
    @Query("SELECT * FROM subject_prov WHERE id == :idSubject")
    suspend fun getSubjectComplete(idSubject : Int): SubjectProvisional
    @Query("SELECT * FROM subject_prov ORDER BY nombre ASC")
    suspend fun getAllSubjects(): List<SubjectProvisional>

    //queries de la tabla Alumno-Asignatura
    @Query("SELECT media FROM student_subject_prov WHERE studentId==:user_id AND subjectId== :subject_id AND trimestre == :trimestre")
    suspend fun getSomeMean(user_id: Int, subject_id: Int, trimestre:Int):Float
    @Query("SELECT trimestre FROM student_subject_prov WHERE studentId==:user_id AND subjectId== :subject_id")
    suspend fun getTrimestres(user_id: Int, subject_id: Int):List<Int>
    @Query("SELECT * FROM student_subject_prov WHERE studentId = :studentID ORDER BY nombre ASC")
    suspend fun  getSubjectsFromStudent(studentID:Int):List<Student_Subject_Provisional>

    @Query("SELECT * FROM student_subject_prov WHERE subjectId = :subjectID")
    suspend fun  getStudentsFromSubject(subjectID:Int):List<Student_Subject_Provisional>

    @Query("SELECT * FROM student_subject_prov WHERE id = :studSubID")
    suspend fun  getSubjectsFromId(studSubID:Int):Student_Subject_Provisional
    @Query("SELECT subjectId FROM student_subject_prov WHERE studentId = :studSubID")
    suspend fun  getSubjectIdFromId(studSubID:Int):List<Int>
    @Query("SELECT subjectId FROM student_subject_prov WHERE subjectId = :studSubID")
    suspend fun  getStudentIdFromId(studSubID:Int):List<Int>
   @Query("SELECT studentId FROM student_subject_prov WHERE subjectId = :subjectID AND studentId =:studentID")
    suspend fun  findStundentOnSubject(studentID:Int, subjectID:Int): Int

    //queries de ponderaciones
    @Query("SELECT name FROM subject_ponderations WHERE subjectId = :subject_ID ORDER BY weight DESC")
    suspend fun getPonderationNames(subject_ID:Int):List<String>
    @Query("SELECT weight FROM subject_ponderations WHERE subjectId = :subject_ID ORDER BY weight DESC")
    suspend fun getPonderationName(subject_ID:Int):List<Float>
    @Query("SELECT * FROM subject_ponderations WHERE id = :pond_ID")
    suspend fun getPonderationComplete(pond_ID:Int):subject_ponderation_provisional
    @Query("SELECT * FROM subject_ponderations WHERE subjectId = :subject_ID")
    suspend fun  getFullPonderation(subject_ID: Int):List<subject_ponderation_provisional>

    @Query("SELECT * FROM student_pond_prov WHERE studentId = :student_id AND subjectId =:subject_ID")
    suspend fun  getAllStudentPonderations(student_id : Int,subject_ID: Int):List<Student_ponderation_provisional>

    @Query("SELECT * FROM student_pond_prov WHERE id = :pondID")
    suspend fun getFUllStudentPond(pondID:Int):Student_ponderation_provisional

    @Query("SELECT * FROM subject_ponderations sp LEFT JOIN student_pond_prov ss ON sp.id = ss.pondId WHERE ss.pondId IS NULL AND ss.studentId = :studentID AND sp.subjectId = :subject_ID ")
    suspend fun getAllSubjectsNotSelected(studentID:Int, subject_ID:Int):List<subject_ponderation_provisional>

    //Medias
   @Query("SELECT ss.weight FROM student_pond_prov sp JOIN subject_ponderations ss ON sp.pondId = ss.id WHERE ss.subjectId = :subjectId AND sp.studentId = :studentId ORDER BY ss.id")
   suspend fun getpondweights(subjectId:Int, studentId:Int):List<Float>

  @Query("SELECT sp.nota FROM student_pond_prov sp JOIN subject_ponderations ss ON sp.pondId = ss.id WHERE ss.subjectId = :subjectId AND sp.studentId = :studentId ORDER BY ss.id")
  suspend fun getpondmarks(subjectId:Int, studentId:Int):List<Float>

  
 @Query("SELECT SUM(ROUND((ss.weight * sp.nota)/100,2)) FROM student_pond_prov sp JOIN subject_ponderations ss ON sp.pondId = ss.id WHERE ss.subjectId = :subjectId AND sp.studentId = :studentId  ORDER BY ss.id")
 suspend fun media(subjectId:Int, studentId:Int):Float
 @Query("SELECT * FROM student_pond_prov sp JOIN subject_ponderations ss ON sp.pondId = ss.id WHERE sp.studentId = :student_id AND ss.subjectId =:subject_ID")
 suspend fun  existingStudentPonderations(student_id : Int,subject_ID: Int):List<Student_ponderation_provisional>
}