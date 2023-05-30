package com.jorgebg.classprobuilder.student

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.TextView
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.jorgebg.classprobuilder.Model.Model
import com.jorgebg.classprobuilder.R
import com.jorgebg.classprobuilder.databases.Student_Subject_Provisional
import com.jorgebg.classprobuilder.databases.SubjectProvisional

class student_signature : AppCompatActivity() {

    lateinit var presenter : presenterStudentSubject
    lateinit var nameSelected : TextView
    lateinit var listSubjects : List<SubjectProvisional>
    lateinit var model : Model

    var idStudent : Int = 0
    var listCoursedSubjects : List<Student_Subject_Provisional> = emptyList()


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_student_subject)

        intent = getIntent()

        nameSelected = findViewById(R.id.selectedStudent)

        nameSelected.text= intent.getStringExtra("nameStudent")


        idStudent=intent.getIntExtra("id",0)

        model = Model(applicationContext)
        presenter = presenterStudentSubject(this , model)

        presenter.getAllSubjects()


    }


    fun initRecycler(lista : List<Student_Subject_Provisional>){

        val recyclerStudentsSubject : RecyclerView = findViewById(R.id.recyclerSubjectName)

        recyclerStudentsSubject.layoutManager = LinearLayoutManager(this)

        val adapter = recyclerAdapterSubjectStud(lista, this)

        recyclerStudentsSubject.adapter = adapter

    }

    fun showDialog(nameToShow: String, subjectStudentId : Int , subjectId: Int, studentId: Int){

        dialogSubject(nameSelected.text as String,nameToShow, subjectStudentId,subjectId,studentId ,presenter).apply{
            show(supportFragmentManager, "dialog")
            setRetainInstance(true)
        }
    }

    fun addSubjectStudent(view: View) {

        dialogInsertSubjectsStudent(this,presenter,idStudent).apply{
            show(supportFragmentManager, "dialog")
            setRetainInstance(true)
        }

    }

    fun getSubjectsComplete(list : List<SubjectProvisional>){

        listSubjects=list
    }


}