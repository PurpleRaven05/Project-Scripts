package com.jorgebg.classprobuilder.student

import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.Button
import android.widget.EditText
import android.widget.Spinner
import androidx.fragment.app.DialogFragment
import com.jorgebg.classprobuilder.R
import com.jorgebg.classprobuilder.databases.Student_Subject_Provisional
import com.jorgebg.classprobuilder.databases.Student_ponderation_provisional
import com.jorgebg.classprobuilder.databases.SubjectProvisional
import com.jorgebg.classprobuilder.databases.subject_ponderation_provisional
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch

class dialogInsertPonderationStud(val view: student_marks, val presenter : presenterStudentMarks,val idStudent : Int, val idSubjectStudent : Int) : DialogFragment(){

    lateinit var newList : ArrayList<String>
    var listNotEmpty = false

    override fun onCreateView(
            inflater: LayoutInflater,
            container: ViewGroup?,
            savedInstanceState: Bundle?
    ): View? {

        var rootView : View = inflater.inflate(R.layout.insert_student_ponderation,container,false)

        val listPonderations : List<subject_ponderation_provisional> = view.listPonds

        newList= arrayListOf()

        for (element in listPonderations){
            newList.add(element.name)
        }
        if(newList.isEmpty()){
            listNotEmpty = false
            newList.add("NONE")
        }

        val spinerSubjects = rootView.findViewById<Spinner>(R.id.spinerSubjects)
        val editMark = rootView.findViewById<EditText>(R.id.editTrimestral)

        val adapter = ArrayAdapter(view.applicationContext, android.R.layout.simple_dropdown_item_1line, newList)

        spinerSubjects.apply {
            setAdapter(adapter)
        }



        val buttonAccept = rootView.findViewById<Button>(R.id.addPonderations)
        buttonAccept.setOnClickListener{

                val index = newList.indexOf(spinerSubjects.selectedItem)
                if(index > -1) {


                    val ponderation = listPonderations.get(index)
                    Log.d("POnderacion selec", ponderation.toString())
                    Log.d("list ponderacion",view.listAsignedPonderations.toString())
                    var exist = false
                    for(element in view.listAsignedPonderations){
                        if(ponderation.id==element.pondId){
                            exist=true
                            break
                        }
                    }
                    if(!exist) {

                        var mark = 0f
                        val stringMark = editMark.text.toString()
                        if (stringMark != "") {
                            mark = stringMark.toFloat()
                        }

                        val addedPonderation = Student_ponderation_provisional(0, ponderation.name, idStudent, idSubjectStudent, ponderation.id, mark)

                        GlobalScope.launch {
                            presenter.insertPondToStudent(addedPonderation)
                            delay(300)
                            presenter.updateMarks()
                            presenter.getPonderationsAsigned()
                        }
                    }
            }
            dialog?.dismiss()
        }


        return rootView
    }
}