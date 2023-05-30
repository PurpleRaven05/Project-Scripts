package com.jorgebg.classprobuilder.subject

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.jorgebg.classprobuilder.Model.Model
import com.jorgebg.classprobuilder.R
import com.jorgebg.classprobuilder.databases.SubjectProvisional
import com.jorgebg.classprobuilder.student.dialogInsertName
import com.jorgebg.classprobuilder.student.dialogNames
import com.jorgebg.classprobuilder.student.presenterStudents
import com.jorgebg.classprobuilder.student.recyclerAdapterStudents

class subject_menu : AppCompatActivity() {

    lateinit var presenter : presenterSubject
    lateinit var model : Model

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_subject_menu)

        model = Model(applicationContext)
        presenter = presenterSubject(this,model)


    }

    fun initRecycler(lista : List<SubjectProvisional>){

        val reyclerSubjects : RecyclerView = findViewById(R.id.recyclerSignatures)

        reyclerSubjects.layoutManager = LinearLayoutManager(this)

        val adapter = reyclerAdapterSubjects(lista, this)

        reyclerSubjects.adapter = adapter



    }

    fun showDialog(nameToShow: String, idSubject:Int){

        dialogNameSubj(nameToShow, idSubject,presenter).apply{
            show(supportFragmentManager, "dialog")
            setRetainInstance(true)
        }
    }

    fun addSubject(view: View) {

        dialogInsertSubject(presenter).apply{
            show(supportFragmentManager, "dialog")
            setRetainInstance(true)
        }

    }


}