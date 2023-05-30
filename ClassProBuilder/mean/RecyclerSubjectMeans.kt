package com.jorgebg.classprobuilder.mean

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.TextView
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.jorgebg.classprobuilder.Model.Model
import com.jorgebg.classprobuilder.R
import com.jorgebg.classprobuilder.databases.StudentProvisional
import com.jorgebg.classprobuilder.student.presenterStudents
import com.jorgebg.classprobuilder.student.recyclerAdapterStudents

class recyclerSubjectMeans : AppCompatActivity() {

    lateinit var presenter : presenterSubjectsMeans
    lateinit var model : Model
    lateinit var texto : TextView

    var result = 0.0

    lateinit var studentName:String
    lateinit var subjectName:String
    var studId =0
    var subId =0

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_recycler_subject_means)


        var studName = intent.getStringExtra("StudentName")
        var subName = intent.getStringExtra("SubjectName")

        studentName= studName.toString()
        subjectName= subName.toString()
        studId = intent.getIntExtra("idStudent",0)
        subId = intent.getIntExtra("idSubject",0)

        texto = findViewById(R.id.nameStudent)
        texto.text=studName
        model = Model(applicationContext)
        presenter = presenterSubjectsMeans(this , model)
    }


    fun initRecycler(nombres : List<String>, marks : List<String>){

        val recyclerSubjectsMeans : RecyclerView = findViewById(R.id.recyclerSubjectMeans)

        recyclerSubjectsMeans.layoutManager = LinearLayoutManager(this)

        val adapter = recyclerAdapterSubjMean(nombres,marks, this)

        recyclerSubjectsMeans.adapter = adapter

    }



}