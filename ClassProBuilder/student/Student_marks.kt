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
import com.jorgebg.classprobuilder.databases.Student_ponderation_provisional
import com.jorgebg.classprobuilder.databases.ponderacion
import com.jorgebg.classprobuilder.databases.subject_ponderation_provisional

class student_marks : AppCompatActivity() {

    lateinit var nameSelected : TextView
    lateinit var subjectSelected : TextView
    lateinit var presenter : presenterStudentMarks
    lateinit var model : Model
    lateinit var listPonds: List<subject_ponderation_provisional>


    var idStudentSubject : Int = 0
    var idSubject : Int = 0
    var idStudent : Int = 0
    var listAsignedPonderations : List<Student_ponderation_provisional> = emptyList()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_student_marks)

        intent = getIntent()

        model = Model(applicationContext)
        presenter = presenterStudentMarks(this , model)

        nameSelected = findViewById(R.id.selectedStudent)
        subjectSelected = findViewById(R.id.subjectSelected)

        nameSelected.text= intent.getStringExtra("nameStudent")
        subjectSelected.text = intent.getStringExtra("nameSubject")

        idStudentSubject = intent.getIntExtra("idSubjectStudent", 0)
        idSubject = intent.getIntExtra("idSubject", 0)
        idStudent = intent.getIntExtra("idStudent", 0)

        presenter.getAllPonderations()
    }


    fun initRecycler(lista : List<Student_ponderation_provisional>){

        val recyclerStudentsMarks : RecyclerView = findViewById(R.id.recyclerStudentMark)

        recyclerStudentsMarks.layoutManager = LinearLayoutManager(this)

        val adapter = recyclerAdapterMarkStudent(lista, this)

        recyclerStudentsMarks.adapter = adapter

    }

    fun showDialog(nameToShow: String, id:Int){

        dialogMarks(nameToShow, idStudentSubject, id, presenter).apply{
            show(supportFragmentManager, "dialog")
            setRetainInstance(true)
        }
    }


    fun addNewPonderation(view: View) {

        dialogInsertPonderationStud(this,presenter,idStudent,idStudentSubject).apply{
            show(supportFragmentManager, "dialog")
            setRetainInstance(true)
        }

    }

    fun getAllponderations(lista : List<subject_ponderation_provisional>){

        listPonds=lista
    }

}