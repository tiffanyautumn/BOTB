import { useState } from "react"
import { useNavigate } from "react-router-dom"
import { AccordionBody, AccordionHeader, AccordionItem, Button } from "reactstrap"
import { ProductIngredientDelete } from "./ProductIngredientDelete"
import { ProductIngredientEdit } from "./ProductIngredientEdit"
import { PIUseForm } from "./ProductIngredientUseForm"

export const ProductIngredient = ({ productIngredient, getProduct, isAdmin, isApproved, getPIs }) => {
    const navigate = useNavigate()
    const [formActive, setFormActive] = useState(false)
    const [deleteActive, setDeleteActive] = useState(false)
    const [useForm, setUseForm] = useState(false)


    return <>
        <AccordionItem>
            <AccordionHeader targetId={productIngredient.id.toString()}>{productIngredient.ingredient.name}</AccordionHeader>
            <AccordionBody accordionId={productIngredient.id.toString()}>
                {
                    formActive || deleteActive
                        ? ""
                        : <>
                            <p className="Review panel-item">
                                <span className="title"><button className="btn" onClick={(() => { navigate(`/ingredient/${productIngredient.ingredientId}`) })}> View Ingredient Details</button>
                                    {
                                        isAdmin
                                            ? <button className="btn" onClick={(() => { setDeleteActive(true) })}><i className="fa-solid fa-trash-can"></i></button>
                                            : ""
                                    }
                                    {
                                        isApproved
                                            ? <button className="btn" onClick={(() => { setFormActive(true) })}><i className="fa-solid fa-eye-dropper"></i></button>
                                            : ""
                                    }
                                </span></p>
                            <section className="ingredient-details">
                                <p className="Review panel-item">
                                    <span className="title">Rating

                                    </span>
                                    {productIngredient.ingredient?.ingredientReview?.rate?.rating ? productIngredient.ingredient?.ingredientReview?.rate?.rating : "not reviewed"}
                                </p>
                                <p className="Review panel-item">
                                    <span className="title">Uses
                                        {isAdmin ? <button onClick={(() => setUseForm(true))} className="btn"><i className="fa-solid fa-plus"></i></button> : ""}
                                    </span>
                                    {productIngredient.uses.map((u) => { return <li key={u.id}>{u.description}</li> })}

                                </p>

                            </section>


                        </>
                }


                {
                    formActive
                        ? <td><ProductIngredientEdit ProductIngredient={productIngredient} setFormActive={setFormActive} getProduct={getProduct} /></td>
                        : ""
                }
                {
                    deleteActive
                        ? <td><ProductIngredientDelete ProductIngredient={productIngredient} setDeleteActive={setDeleteActive} getProduct={getProduct} getPIs={getPIs} /></td>
                        : ""
                }
                {
                    useForm ? <PIUseForm getPIs={getPIs} ProductIngredient={productIngredient} setUseForm={setUseForm} /> : ""
                }
            </AccordionBody>
        </AccordionItem>
    </>
}