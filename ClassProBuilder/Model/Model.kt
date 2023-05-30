package com.jorgebg.classprobuilder.Model

import android.content.Context
import android.util.Log
import androidx.lifecycle.ViewModel
import com.android.volley.Response
import com.jorgebg.classprobuilder.databases.*
import kotlinx.coroutines.*
import kotlinx.coroutines.android.awaitFrame

class Model(context: Context) : ViewModel() {

    val dbDao = provisionalDatabase.getDatabase(context).getDao()

    fun addStudents(student: StudentProvisional) = GlobalScope.launch(Dispatchers.Main) {

        withContext(Dispatchers.IO){
            dbDao.addStudent(student)
        }

    }

    fun getStudents(listener: Response.Listener<List<StudentProvisional>>, errorListener: Response.ErrorListener) = GlobalScope.launch(Dispatchers.Main) {

        val listNames = withContext(Dispatchers.IO) {
            dbDao.getAllStudents()
        }
        listener.onResponse(listNames)

    }

    fun removeStudent(name : String) = GlobalScope.launch(Dispatchers.Main){

        val subject = dbDao.getStudentComplete(name)
        dbDao.deleteStudent(subject)

    }

    ////////Subjects

    fun addSubject(nameSubject : String) = GlobalScope.launch(Dispatchers.Main){

        val subject = withContext(Dispatchers.IO)
        {
            SubjectProvisional(0, nameSubject,0)
        }
        dbDao.addSubject(subject)
    }

    fun getSubjects(listener: Response.Listener<List<SubjectProvisional>>, errorListener: Response.ErrorListener) = GlobalScope.launch(Dispatchers.Main){


        val listNames = withContext(Dispatchers.IO) {
            dbDao.getAllSubjects()
        }

        listener.onResponse(listNames)
    }

    fun removeSubject(idSubject : Int) = GlobalScope.launch(Dispatchers.Main){


        val subject = withContext(Dispatchers.IO) {
            dbDao.getSubjectComplete(idSubject)
        }
        dbDao.deleteSubject(subject)

    }

    fun addSubjectToStudent(subject : Student_Subject_Provisional) = GlobalScope.launch(Dispatchers.Main){

        withContext(Dispatchers.IO)
        {
            dbDao.addStudent_Subject(subject)
        }
    }

    fun getAllSubjectsOfStudent(listener: Response.Listener<List<Student_Subject_Provisional>>, errorListener: Response.ErrorListener, subjectid: Int) = GlobalScope.launch(Dispatchers.Main){

        val listNames = withContext(Dispatchers.IO) {
            dbDao.getStudentsFromSubject(subjectid)
        }
        listener.onResponse(listNames)
    }

    fun getSubjectsOfStudent(listener: Response.Listener<List<Student_Subject_Provisional>>, errorListener: Response.ErrorListener, studentId : Int) = GlobalScope.launch(Dispatchers.Main){

        val listNames = withContext(Dispatchers.IO) {
            dbDao.getSubjectsFromStudent(studentId)
        }
        listener.onResponse(listNames)
    }

    fun removeSubjectOfStudent(idStudentSubject : Int) = GlobalScope.launch(Dispatchers.Main){

        withContext(Dispatchers.IO) {
            val subjectStudent = dbDao.getSubjectsFromId(idStudentSubject)
            dbDao.deleteStudent_Subject(subjectStudent)
        }
    }


    //Ponderations
    fun getPondsFromSubject(subject_ID:Int, listener: Response.Listener<List<subject_ponderation_provisional>>, errorListener: Response.ErrorListener) = GlobalScope.launch(Dispatchers.Main) {

        val listPonderations = withContext(Dispatchers.IO){
            dbDao.getFullPonderation(subject_ID)
        }
        listener.onResponse(listPonderations)

    }
    fun getSelectablePonderations(subject_ID:Int, studentId: Int, listener: Response.Listener<List<subject_ponderation_provisional>>, errorListener: Response.ErrorListener) = GlobalScope.launch(Dispatchers.Main) {

        val listPonderations = withContext(Dispatchers.IO){
            dbDao.getAllSubjectsNotSelected(studentId,subject_ID)
        }
        listener.onResponse(listPonderations)
        Log.d("prieba",listPonderations.toString())

    }
    fun addPonderation(subject_id:Int ,namePond : String, peso:Float) = GlobalScope.launch(Dispatchers.Main){

        withContext(Dispatchers.IO)
        {
            dbDao.addSubjectPonderation(subject_ponderation_provisional(0, subject_id, namePond,peso))
        }
    }

    fun deletePonderation(id:Int) = GlobalScope.launch(Dispatchers.Main){


        withContext(Dispatchers.IO)
        {
            val pondSel= dbDao.getPonderationComplete(id)
            dbDao.deleteSubjectPonderation(pondSel)
        }
    }

    fun insertPonderationStudent(stundentPond : Student_ponderation_provisional) = GlobalScope.launch(Dispatchers.Main){

        withContext(Dispatchers.IO)
        {
            dbDao.addStudentPonderations(stundentPond)
        }

    }

    fun getStudentPonderations( listener: Response.Listener<List<Student_ponderation_provisional>>, errorListener: Response.ErrorListener,idStudent : Int, idStudentSubject : Int) = GlobalScope.launch(Dispatchers.Main) {

        val listNames = withContext(Dispatchers.IO) {
            dbDao.getAllStudentPonderations(idStudent,idStudentSubject)
        }
        listener.onResponse(listNames)

    }
    fun getExistingStudentPonderations( listener: Response.Listener<List<Student_ponderation_provisional>>, errorListener: Response.ErrorListener,idStudent : Int, idSubject : Int) = GlobalScope.launch(Dispatchers.Main) {

        val listNames = withContext(Dispatchers.IO) {
            dbDao.existingStudentPonderations(idStudent,idSubject)
        }
        listener.onResponse(listNames)

    }
    fun deleteStudentPonderation(id:Int) = GlobalScope.launch(Dispatchers.Main) {
        withContext(Dispatchers.IO){
            val pondProv = dbDao.getFUllStudentPond(id)
            dbDao.deleteStudentPonderation(pondProv)
        }

    }
    fun getAllWeights(idStudent: Int, idSubject: Int,listener: Response.Listener<List<Float>>, errorListener: Response.ErrorListener ) = GlobalScope.launch(Dispatchers.Main) {
        val listweights = withContext(Dispatchers.IO){
            dbDao.getpondweights(idSubject, idStudent)
        }
        listener.onResponse(listweights)
    }
    fun getAllMarks(idStudent: Int, idSubject: Int,listener: Response.Listener<List<Float>>, errorListener: Response.ErrorListener ) = GlobalScope.launch(Dispatchers.Main) {
        val listmarks = withContext(Dispatchers.IO){
            dbDao.getpondmarks(idSubject, idStudent)
        }
        listener.onResponse(listmarks)
    }

    fun getStudentFromId(idStudent: Int,listener: Response.Listener<String>, errorListener: Response.ErrorListener ) = GlobalScope.launch(Dispatchers.Main) {
        val name = withContext(Dispatchers.IO){
            dbDao.showName(idStudent)
        }
        listener.onResponse(name)
    }
    fun getMean(idStudent: Int, idSubject: Int,listener: Response.Listener<Float>, errorListener: Response.ErrorListener ) = GlobalScope.launch(Dispatchers.Main) {
        val mark = withContext(Dispatchers.IO){
            dbDao.media(idSubject, idStudent)
        }

        listener.onResponse(mark)
    }

    fun searchStudentOnSubject(listener: Response.Listener<Int>, errorListener: Response.ErrorListener,studentID : Int, subjectID : Int) = GlobalScope.launch(Dispatchers.Main) {

        withContext(Dispatchers.IO){
            listener.onResponse(dbDao.findStundentOnSubject(studentID, subjectID))
        }


    }

    fun updateStudent(student:StudentProvisional)= GlobalScope.launch(Dispatchers.Main){
        withContext(Dispatchers.IO){
            dbDao.updateStudent(student)
        }

    }







}