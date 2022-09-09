import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Food, FoodAccessory } from 'generated/models';
import { FoodSize } from 'generated/models/food-size';
import { FoodService, ShoppingCartService } from 'services';

@Component({
  selector: 'food-page',
  templateUrl: './food-page.html',
})
export class FoodPageComponent implements OnInit {
  isLoading: boolean = false;
  food: Food = {};
  sizes: FoodSize[] = Object.values(FoodSize);
  form: FormGroup = this.formBuilder.group({
    id: 0,
    quantity: 1,
    accessories: this.formBuilder.array([]),
    size: FoodSize.Small,
  });

  get price() {
    const temp =
      1 +
      Number(this.form.value.size === FoodSize.Middle) +
      2 * Number(this.form.value.size === FoodSize.Big);
    return (
      this.form.value.quantity *
      (temp * (this.food.price ?? 0) +
        this.form.value.accessories.reduce(
          (x: FoodAccessory, y: FoodAccessory) =>
            temp * (x.price ?? 0) + (y.price ?? 0),
          0
        ))
    );
  }

  constructor(
    private shoppingCartService: ShoppingCartService,
    private foodService: FoodService,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      const id = Number(params['id']);
      this.isLoading = true;
      this.getFoodEntry(id).add(() => {
        this.isLoading = false;
        this.form.patchValue({ ...this.form.value, food: this.food, id });
      });
    });
  }

  handleSubmit() {
    this.shoppingCartService
      .addItem({
        body: {
          size: this.form.value.size,
          foodId: this.form.value.id,
          quantity: this.form.value.quantity,
          accessories: this.form.value.accessories as FoodAccessory[],
        },
      })
      .subscribe({
        next: () => {},
        error: (error) => {
          console.log(error);
        },
      });
  }

  handleAccessoriesChange(event: Event) {
    const target = event.target as HTMLInputElement;
    const control = this.form.controls['accessories'] as FormArray;
    const id = Number(target.value);
    if (target.checked) {
      control.push(
        new FormControl(this.food.accessories?.find((entry) => entry.id === id))
      );
    } else {
      control.controls.forEach((item, i) => {
        if (item.value.id === id) {
          control.removeAt(i);
        }
      });
    }
  }

  getFoodEntry(id: number) {
    return this.foodService.getFoodEntry$Json({ id }).subscribe({
      next: (response) => {
        this.food = response;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
