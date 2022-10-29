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
                                    {productIngredient.uses.map((u) => { return <li>{u.description}</li> })}

                                </p>
                                <Button onClick={(() => { navigate(`/ingredient/${productIngredient.ingredientId}`) })}>Details</Button>
                            </section>
                        </>
                }
                {
                    isAdmin
                        ? <td><Button onClick={(() => { setDeleteActive(true) })}>Delete</Button></td>
                        : ""
                }
                {
                    isApproved
                        ? <td><Button onClick={(() => { setFormActive(true) })}>Edit</Button></td>
                        : ""
                }

                {
                    formActive
                        ? <td><ProductIngredientEdit ProductIngredient={productIngredient} setFormActive={setFormActive} getProduct={getProduct} /></td>
                        : ""
                }
                {
                    deleteActive
                        ? <td><ProductIngredientDelete ProductIngredient={productIngredient} setDeleteActive={setDeleteActive} getProduct={getProduct} /></td>
                        : ""
                }
                {
                    useForm ? <PIUseForm getPIs={getPIs} ProductIngredient={productIngredient} setUseForm={setUseForm} /> : ""
                }
            </AccordionBody>
        </AccordionItem>
    </>
}