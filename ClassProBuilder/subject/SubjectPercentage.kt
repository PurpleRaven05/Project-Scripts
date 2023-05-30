package com.jorgebg.classprobuilder.subject

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.TextView
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.jorgebg.classprobuilder.Model.Model
import com.jorgebg.classprobuilder.R
import com.jorgebg.classprobuilder.databases.subject_ponderation_provisional

class subjectPercentage : AppCompatActivity() {


    lateinit var presenter : presenterSubjectPercentaje
    lateinit var model : Model

    lateinit var nameSelected : TextView
    var subjectId : Int = 0

    val prueba : List<String> =
            listOf( "A",
                    "B",
                    "C",
                    "D")


    override fun onCreate(savedInstanceState: Bundle?) {

        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_subject_percentage)

        intent = getIntent()
        nameSelected = findViewById(R.id.subjectName)


        nameSelected.text= intent.getStringExtra("nameSubject")
        subjectId = intent.getIntExtra("idSubject",0)


        model = Model(applicationContext)
        presenter = presenterSubjectPercentaje(this,model)


    }

    fun initRecycler(list: List<subject_ponderation_provisional>){

        val reyclerPercentage : RecyclerView = findViewById(R.id.recyclerPercentages)

        reyclerPercentage.layoutManager = LinearLayoutManager(this)

        val adapter = recyclerAdapterPercentage(list, this)

        reyclerPercentage.adapter = adapter

    }

    fun showDialog(nameToShow: String, idToShow:Int){

        dialogDeletePercentage(nameToShow, idToShow, presenter).apply{
            show(supportFragmentManager, "dialog")
            setRetainInstance(true)
        }
    }

    fun addPonderation(view: View) {

        dialogInsertPonderation(presenter).apply{
            show(supportFragmentManager, "dialog")
            setRetainInstance(true)
        }

    }



}

