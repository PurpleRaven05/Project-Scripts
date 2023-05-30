package com.jorgebg.classprobuilder.mean

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.TextView
import com.jorgebg.classprobuilder.Model.Model
import com.jorgebg.classprobuilder.R
import org.w3c.dom.Text

class specificMeanCopy : AppCompatActivity() {


    lateinit var presenter : specificPresenter
    lateinit var model : Model

    //textos a editar
    lateinit var MarkText: TextView
    lateinit var StudentName: TextView
    lateinit var SubjectName: TextView

    //listas para la media
    var result = 0.0f

    //variables pasadas
    //lateinit var studName:String
    //lateinit var subName:String
    var studId =0
    var subId =0

    override fun onCreate(savedInstanceState: Bundle?) {

        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_media_alumno_asignatura_copia)

        StudentName=findViewById(R.id.student_name)
        SubjectName=findViewById(R.id.subject_text)
        MarkText=findViewById(R.id.subject_mark)


        var studName = intent.getStringExtra("StudentName")
        Log.d("Index5",studName.toString())
        var subName = intent.getStringExtra("SubjectName")
        studId = intent.getIntExtra("idStudent",0)
        subId = intent.getIntExtra("idSubject",0)

        Log.d("Nombre", studName.toString())

        StudentName.text=studName
        SubjectName.text=subName


        model = Model(applicationContext)
        presenter = specificPresenter(this , model)

    }
    fun mean(mark:Float){
        result=mark
        MarkText.text=result.toString()
    }

}