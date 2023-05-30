package com.jorgebg.classprobuilder.mean

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.TextView
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.jorgebg.classprobuilder.Model.Model
import com.jorgebg.classprobuilder.R

class studeantSubjectMean : AppCompatActivity() {

    lateinit var presenter : presenterStudentsMeans
    lateinit var model : Model
    lateinit var texto : TextView

    lateinit var studentName:String
    lateinit var subjectName:String
    var studId =0
    var subId =0

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_recycler_studeant_subject_mean)



        var studName = intent.getStringExtra("StudentName")
        var subName = intent.getStringExtra("SubjectName")

        studentName= studName.toString()
        subjectName= subName.toString()
        studId = intent.getIntExtra("idStudent",0)
        subId = intent.getIntExtra("idSubject",0)

        texto = findViewById(R.id.nameSubject)
        texto.text=subjectName
        model = Model(applicationContext)
        presenter = presenterStudentsMeans(this , model)
    }


    fun initRecycler(nombres : List<String>, marks : List<String>){

        val recyclerStudentsMeans : RecyclerView = findViewById(R.id.recyclerStudentMeans)

        recyclerStudentsMeans.layoutManager = LinearLayoutManager(this)

        val adapter = recyclerAdapterStudentsMean(nombres,marks, this)

        recyclerStudentsMeans.adapter = adapter

    }
}